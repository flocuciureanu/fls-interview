using FirstLineSoftwareInterviewTask.Common.Core.ExtensionMethods;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.CreateUserRequest.FirstName).NotEmpty();
            
            RuleFor(command => command.CreateUserRequest.LastName).NotEmpty();
            
            RuleFor(command => command.CreateUserRequest.EmailAddress)
                .NotEmpty()
                .Must(CheckEmailIsValid).WithMessage(CommandResultCustomNotificationMessages.InvalidEmailAddress);
        }

        private static bool CheckEmailIsValid(string emailAddress)
            => !string.IsNullOrEmpty(emailAddress) && emailAddress.IsValidEmailAddress();
    }
}