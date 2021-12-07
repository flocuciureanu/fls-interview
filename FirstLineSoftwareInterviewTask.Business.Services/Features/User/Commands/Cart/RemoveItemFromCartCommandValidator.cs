using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
    {
        public RemoveItemFromCartCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            
            RuleFor(command => command.ItemId).NotEmpty();

            RuleFor(command => command.Count).Must(x => x > 0)
                .WithMessage(CommandResultCustomNotificationMessages.InvalidItemCount);
        }
    }
}