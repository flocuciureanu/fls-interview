using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Commands
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(command => command.CreateItemRequest.Title).NotEmpty();         
            
            RuleFor(command => command.CreateItemRequest.Pricing).NotEmpty();
            
            RuleFor(command => command.CreateItemRequest.Pricing.PriceAmount).Must(x => x > 0)
                .WithMessage(CommandResultCustomNotificationMessages.InvalidPriceAmount);
        }
    }
}