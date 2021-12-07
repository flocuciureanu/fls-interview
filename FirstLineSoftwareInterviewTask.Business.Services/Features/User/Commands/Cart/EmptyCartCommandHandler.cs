using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using MediatR;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class EmptyCartCommandHandler : IRequestHandler<EmptyCartCommand, ICommandResult>
    {  
        private readonly ICommandResultFactory _commandResultFactory;
        private readonly IUserService _userService;

        public EmptyCartCommandHandler(ICommandResultFactory commandResultFactory, IUserService userService)
        {
            _commandResultFactory = commandResultFactory;
            _userService = userService;
        }

        public async Task<ICommandResult> Handle(EmptyCartCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<UserCollection>.Filter.Where(x => x.Id.Equals(request.UserId));

            var update = Builders<UserCollection>.Update.Set(x => x.CartDetails,
                new CartDetails() { CartItems = new List<CartItem>() });

            var updatedUser = await _userService.FindOneAndUpdateAsync(filter, update);

            return updatedUser is null
                ? _commandResultFactory.Create(false, StatusCodes.Status400BadRequest)
                : _commandResultFactory.Create(true, CommandResultCustomNotificationMessages.UserCartSuccessfullyEmpty);
        }
    }
}