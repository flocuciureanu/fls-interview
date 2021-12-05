using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class GetCartDetailsCommand : IQuery<ICommandResult>
    {
        public string UserId { get; set; }
    }
}