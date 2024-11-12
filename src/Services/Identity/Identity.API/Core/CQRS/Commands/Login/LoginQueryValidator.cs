using FluentValidation;

namespace Identity.API.Core.CQRS.Commands.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginCommand>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
