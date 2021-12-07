using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class AddItemToUserCartCommandValidator : AbstractValidator<AddItemToUserCartCommand>
    {
        public AddItemToUserCartCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            
            RuleFor(command => command.ItemId).NotEmpty();
            
            RuleFor(command => command.Count).Must(x => x > 0)
                .WithMessage(CommandResultCustomNotificationMessages.InvalidItemCount);
        }
    }
}