using FluentValidation;
using Products.API.Core.CQRS.Commands;

namespace Products.API.Core.CQRS.Validators
{
    public class AddTagsToProductValidator : AbstractValidator<AddTagsToProductCommand>
    {
        public AddTagsToProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
