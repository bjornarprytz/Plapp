using MediatR;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class SaveTopicCommand : IRequestWrapper<Topic> 
    {
        public Topic Topic { get; set; }
    }

    public class SaveTopicCommandHandler : IHandlerWrapper<SaveTopicCommand, Topic>
    {
        private readonly ITopicService _topicService;

        public SaveTopicCommandHandler(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public async Task<Response<Topic>> Handle(SaveTopicCommand request, CancellationToken cancellationToken)
        {
            return Response.Ok(await _topicService.SaveAsync(request.Topic, cancellationToken));
        }
    }

}
