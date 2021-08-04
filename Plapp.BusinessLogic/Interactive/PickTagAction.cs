using MediatR;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Interactive
{
    public class PickTagAction : IRequestWrapper<ITagViewModel>
    {
    }

    public class PickTagActionHandler : IHandlerWrapper<PickTagAction, ITagViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IPrompter _prompter;

        public PickTagActionHandler(
            IMediator mediator,
            IPrompter prompter)
        {
            _mediator = mediator;
            _prompter = prompter;
        }

        public async Task<IResponseWrapper<ITagViewModel>> Handle(PickTagAction request, CancellationToken cancellationToken)
        {
            var tagResponse = await _mediator.Send(new GetAllTagsQuery(), cancellationToken);

            if (!tagResponse.IsValid)
                return tagResponse.WrapErrors<ITagViewModel>();

            var existingTags = tagResponse.Payload;

            var options = new List<string> { "Create new Tag" };

            options.AddRange(existingTags.Select(t => t.Key));

            var choice = await _prompter.ChooseAsync("Choose a Tag", "Cancel", null, options.ToArray());

            var chosenTag = choice switch
            {
                "Cancel" => default,
                "Create new Tag" => await _prompter.CreateAsync<ITagViewModel>(),
                _ => existingTags.First(t => t.Key == choice)
            };

            if (chosenTag == default)
            {
                return null;
            }

            var saveResponse = await _mediator.Send(new SaveTagCommand(chosenTag), cancellationToken);

            return saveResponse.IsValid ? Response.Ok(chosenTag) : tagResponse.WrapErrors<ITagViewModel>();
        }
    }
}
