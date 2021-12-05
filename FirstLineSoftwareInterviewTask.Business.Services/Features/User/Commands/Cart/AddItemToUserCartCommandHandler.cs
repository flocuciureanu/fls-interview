using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.Cart;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services;
using FirstLineSoftwareInterviewTask.Common.Core.ExtensionMethods;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class AddItemToUserCartCommandHandler : IRequestHandler<AddItemToUserCartCommand, ICommandResult>
    {
        private readonly ICommandResultFactory _commandResultFactory;
        private readonly IUserService _userService;
        private readonly ICartDetailsBuilder _cartDetailsBuilder;
        private readonly ICartItemBuilder _cartItemBuilder;
        private readonly IItemService _itemService;
        private readonly IResponseBuilder<CartDetailsResponse> _responseBuilder;

        public AddItemToUserCartCommandHandler(ICommandResultFactory commandResultFactory, 
            IUserService userService,
            ICartDetailsBuilder cartDetailsBuilder, 
            ICartItemBuilder cartItemBuilder,
            IItemService itemService, 
            IResponseBuilder<CartDetailsResponse> responseBuilder)
        {
            _commandResultFactory = commandResultFactory;
            _userService = userService;
            _cartDetailsBuilder = cartDetailsBuilder;
            _cartItemBuilder = cartItemBuilder;
            _itemService = itemService;
            _responseBuilder = responseBuilder;
        }

        public async Task<ICommandResult> Handle(AddItemToUserCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user is null)
                return _commandResultFactory.Create(false, StatusCodes.Status401Unauthorized);

            var itemFromDb = await _itemService.GetByIdAsync(request.ItemId);
            if (itemFromDb is null)
                return _commandResultFactory.Create(false, StatusCodes.Status404NotFound);

            var userFilter = Builders<UserCollection>.Filter.Where(x => x.Id.Equals(request.UserId));
            var updateList = new List<UpdateDefinition<UserCollection>>();

            // Create cart and add item
            if (user.CartDetails == null || !user.CartDetails.CartItems.HasValue())
            {
                var itemToAdd = GetItemToAdd(request.Count, itemFromDb);

                var cartItems = new List<CartItem>() { itemToAdd };
                
                var cartDetails = _cartDetailsBuilder
                    .WithCartItems(cartItems)
                    .Build();
                
                updateList.Add(Builders<UserCollection>.Update.Set(x => x.CartDetails, cartDetails));
            }
            else
            {
                var cartItem = user.CartDetails.CartItems.FirstOrDefault(x => x.ItemId.Equals(request.ItemId));
                
                if (cartItem is null)
                {
                    var itemToAdd = GetItemToAdd(request.Count, itemFromDb);
                    
                    updateList.Add(Builders<UserCollection>.Update.Push(x => x.CartDetails.CartItems, itemToAdd));
                }
                else
                {
                    cartItem.Count += request.Count;
                    
                    updateList.Add(Builders<UserCollection>.Update.Set(x => x.CartDetails.CartItems, user.CartDetails.CartItems));
                }
            }

            var update = Builders<UserCollection>.Update.Combine(updateList);

            var updatedUser = await _userService.FindOneAndUpdateAsync(userFilter, update);

            return updatedUser is null
                ? _commandResultFactory.Create(false, StatusCodes.Status400BadRequest)
                : _commandResultFactory.Create(true, _responseBuilder
                    .Map(updatedUser.CartDetails)
                    .Build());
        }

        private CartItem GetItemToAdd(int count, ItemCollection itemFromDb)
        {
            return _cartItemBuilder
                .WithItemId(itemFromDb.Id)
                .WithCount(count)
                .Build();        
        }
    }
}