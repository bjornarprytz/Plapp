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

            var existingTags = tagResponse.Payload.ToList();

            var options = existingTags.Select(t => t.Key).ToArray();

            var choice = await _prompter.ChooseAsync("Choose a Tag", "Cancel", null, options);

            var chosenTag = choice switch
            {
                "Cancel" => default,
                _ => existingTags.First(t => t.Key == choice)
            };

            return chosenTag == default ? Response.Cancel<ITagViewModel>() : Response.Ok(chosenTag);

        }
    }
}
