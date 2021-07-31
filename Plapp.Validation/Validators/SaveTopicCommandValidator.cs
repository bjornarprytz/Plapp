using FluentValidation;
using Plapp.BusinessLogic.Commands;

namespace Plapp.Validation
{
    public class SaveTopicCommandValidator : AbstractValidator<SaveTopicCommand>
    {
        public SaveTopicCommandValidator()
        {
            RuleFor(x => x.Topic.Title).NotEmpty();
        }
    }
}