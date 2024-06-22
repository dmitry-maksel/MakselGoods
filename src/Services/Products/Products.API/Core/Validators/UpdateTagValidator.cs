using FluentValidation;
using Products.API.Core.Queries.Tags;

namespace Products.API.Core.Validators
{
    public class UpdateTagValidator : AbstractValidator<UpdateTagQuery>
    {
        public UpdateTagValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        }
    }
}
