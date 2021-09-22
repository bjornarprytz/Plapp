using FluentValidation;
using Plapp.BusinessLogic.Commands;
using Plapp.Core;

namespace Plapp.Validation.Validators
{
    public class SaveTagCommandValidator : AbstractValidator<SaveTagCommand>
    {
        public SaveTagCommandValidator()
        {
            RuleFor(x => x.Tag.Key).NotEmpty();
            RuleFor(x => x.Tag.Unit).NotEmpty();
        }
    }
}