using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services;
using FirstLineSoftwareInterviewTask.Common.Core.ExtensionMethods;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class GetCartDetailsCommandHandler : IQueryHandler<GetCartDetailsCommand, ICommandResult>
    {
        private readonly ICommandResultFactory _commandResultFactory;
        private readonly IUserService _userService;
        private readonly IResponseBuilder<CartDetailsResponse> _responseBuilder;

        public GetCartDetailsCommandHandler(ICommandResultFactory commandResultFactory, 
            IUserService userService, IResponseBuilder<CartDetailsResponse> responseBuilder)
        {
            _commandResultFactory = commandResultFactory;
            _userService = userService;
            _responseBuilder = responseBuilder;
        }

        public async Task<ICommandResult> Handle(GetCartDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user is null)
                return _commandResultFactory.Create(false, StatusCodes.Status401Unauthorized);

            if (user.CartDetails == null || !user.CartDetails.CartItems.HasValue())
                return _commandResultFactory.Create(true, CommandResultCustomNotificationMessages.EmptyUserCart);

            var response = _responseBuilder.Map(user.CartDetails).Build();
            
            return _commandResultFactory.Create(true, response);
        }
    }
}