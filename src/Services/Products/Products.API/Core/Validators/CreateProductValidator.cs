using FluentValidation;
using Products.API.Core.Queries.Products;

namespace Products.API.Core.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductQuery>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).MaximumLength(300);
        }
    }
}
