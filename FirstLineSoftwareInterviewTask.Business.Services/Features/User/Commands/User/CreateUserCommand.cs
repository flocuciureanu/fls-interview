using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Requests.User;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.User
{
    public class CreateUserCommand : ICommand<ICommandResult>
    {
        public CreateUserRequest CreateUserRequest { get; set; }
    }
}