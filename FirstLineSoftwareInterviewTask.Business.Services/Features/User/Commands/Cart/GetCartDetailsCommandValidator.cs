using FluentValidation;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart
{
    public class GetCartDetailsCommandValidator : AbstractValidator<GetCartDetailsCommand>
    {
        public GetCartDetailsCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}