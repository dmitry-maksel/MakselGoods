using FluentValidation;
using Products.API.Core.Queries.Products;

namespace Products.API.Core.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductQuery>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).MaximumLength(300);
        }
    }
}
