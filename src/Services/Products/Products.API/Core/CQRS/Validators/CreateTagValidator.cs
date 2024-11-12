using FluentValidation;
using Products.API.Core.CQRS.Commands;

namespace Products.API.Core.CQRS.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        }
    }
}
