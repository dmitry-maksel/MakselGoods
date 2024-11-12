using FluentValidation;
using Products.API.Core.CQRS.Commands;

namespace Products.API.Core.CQRS.Validators
{
    public class UpdateTagValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        }
    }
}
