using MediatR;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class DeleteTopicCommand : IRequestWrapper
    {
        public DeleteTopicCommand(ITopicViewModel topic)
        {
            Topic = topic;
        }

        public ITopicViewModel Topic { get; private set; }
    }

    public class DeleteTopicCommandHandler : IHandlerWrapper<DeleteTopicCommand>
    {
        private readonly ITopicService _topicService;

        public DeleteTopicCommandHandler(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public async Task<Response<Unit>> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            await _topicService.DeleteAsync(request.Topic.Id, cancellationToken);

            return Response.Ok();
        }
    }
}
