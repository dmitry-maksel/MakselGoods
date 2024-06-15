using FluentValidation;
using Identity.API.Core.Queries;
using Identity.API.Extensions;

namespace Identity.API.Core.Validators
{
    public class SignUpQueryValidator : AbstractValidator<SignUpQuery>
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
