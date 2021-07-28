using AutoMapper;
using MediatR;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class SaveTopicCommand : IRequestWrapper
    {
        public ITopicViewModel Topic { get; private set; }

        public SaveTopicCommand(ITopicViewModel topic)
        {
            Topic = topic;
        }
    }

    public class SaveTopicCommandHandler : IHandlerWrapper<SaveTopicCommand>
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public SaveTopicCommandHandler(
            ITopicService topicService,
            IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(SaveTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = _mapper.Map<Topic>(request.Topic);

            await _topicService.SaveAsync(topic, cancellationToken);

            request.Topic.Id = topic.Id;

            return Response.Ok();
        }
    }

}
