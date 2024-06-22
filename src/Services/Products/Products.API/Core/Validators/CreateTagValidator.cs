using FluentValidation;
using Products.API.Core.Queries.Tags;

namespace Products.API.Core.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTagQuery>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        }
    }
}
