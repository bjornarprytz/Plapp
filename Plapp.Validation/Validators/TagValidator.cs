using FluentValidation;
using Plapp.Core;

namespace Plapp.Validation.Validators
{
    public class TagValidator : AbstractValidator<ITagViewModel>
    {
        public TagValidator()
        {
            RuleFor(x => x.Key).NotEmpty();
        }
        
    }
}