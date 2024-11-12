using FluentValidation;
using Identity.API.Extensions;

namespace Identity.API.Core.CQRS.Commands.SignUp
{
    public class SignUpQueryValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpQueryValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).Password();
        }
    }
}
