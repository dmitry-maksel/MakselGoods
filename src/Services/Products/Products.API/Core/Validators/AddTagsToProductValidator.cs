using FluentValidation;
using Products.API.Core.Queries;

namespace Products.API.Core.Validators
{
    public class AddTagsToProductValidator : AbstractValidator<AddTagsToProductQuery>
    {
        public AddTagsToProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
