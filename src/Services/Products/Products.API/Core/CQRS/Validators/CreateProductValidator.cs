using FluentValidation;
using Products.API.Core.CQRS.Commands;

namespace Products.API.Core.CQRS.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).MaximumLength(300);
        }
    }
}
