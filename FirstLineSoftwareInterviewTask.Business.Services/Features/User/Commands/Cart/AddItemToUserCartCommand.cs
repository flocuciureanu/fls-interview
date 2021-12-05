using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class AddItemToUserCartCommand : ICommand<ICommandResult>
    {
        public string UserId { get; set; }
        public string ItemId { get; set; }
        public int Count { get; set; }
    }
}