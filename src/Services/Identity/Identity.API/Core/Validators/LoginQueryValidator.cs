using FluentValidation;
using Identity.API.Core.Queries;

namespace Identity.API.Core.Validators
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
