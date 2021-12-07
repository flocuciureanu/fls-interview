namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class EmptyCartCommandValidator : AbstractValidator<EmptyCartCommand>
    {
        public EmptyCartCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}