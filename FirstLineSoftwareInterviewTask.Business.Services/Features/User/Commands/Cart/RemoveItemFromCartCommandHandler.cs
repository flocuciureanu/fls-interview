using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services;
using FirstLineSoftwareInterviewTask.Common.Core.ExtensionMethods;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, ICommandResult>
    {
        private readonly ICommandResultFactory _commandResultFactory;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        private readonly IResponseBuilder<CartDetailsResponse> _responseBuilder;

        public RemoveItemFromCartCommandHandler(ICommandResultFactory commandResultFactory, 
            IUserService userService, 
            IItemService itemService, 
            IResponseBuilder<CartDetailsResponse> responseBuilder)
        {
            _commandResultFactory = commandResultFactory;
            _userService = userService;
            _itemService = itemService;
            _responseBuilder = responseBuilder;
        }

        public async Task<ICommandResult> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user is null)
                return _commandResultFactory.Create(false, StatusCodes.Status401Unauthorized);

            if (user.CartDetails == null || !user.CartDetails.CartItems.HasValue())
                return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest, CommandResultCustomNotificationMessages.EmptyUserCart);

            var cartItem = user.CartDetails.CartItems.FirstOrDefault(x => x.ItemId.Equals(request.ItemId));
            if (cartItem is null)
                return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest, CommandResultCustomNotificationMessages.ItemNotInCart);

            var newCount = cartItem.Count - request.Count;
            if (newCount < 0)
                return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest, $"You can only remove a maximum of {cartItem.Count} items from your cart");
            
            var itemFromDb = await _itemService.GetByIdAsync(request.ItemId);
            if (itemFromDb is null)
                return _commandResultFactory.Create(false, StatusCodes.Status404NotFound);
            
            var userFilter = Builders<UserCollection>.Filter.Where(x => x.Id.Equals(request.UserId));
            var updateList = new List<UpdateDefinition<UserCollection>>();
            
            switch (newCount)
            {
                case 0:
                    var cartItems = user.CartDetails.CartItems.Remove(cartItem);

                    if (cartItems)
                        updateList.Add(Builders<UserCollection>.Update.Set(x => x.CartDetails.CartItems, user.CartDetails.CartItems));
                    else
                        return _commandResultFactory.Create(false);
                    break;

                case > 0:
                    cartItem.Count = newCount;
                
                    updateList.Add(Builders<UserCollection>.Update.Set(x => x.CartDetails.CartItems, user.CartDetails.CartItems));
                    break;
            }

            var update = Builders<UserCollection>.Update.Combine(updateList);

            var updatedUser = await _userService.FindOneAndUpdateAsync(userFilter, update);

            return updatedUser is null
                ? _commandResultFactory.Create(false, StatusCodes.Status400BadRequest)
                : _commandResultFactory.Create(true, _responseBuilder
                    .Map(updatedUser.CartDetails)
                    .Build());
        }
    }
}