using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Requests;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Commands
{
    public class CreateItemCommand : ICommand<ICommandResult>
    {
        public CreateItemRequest CreateItemRequest { get; set; }
    }
}