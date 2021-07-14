using MediatR;
using Plapp.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Queries
{
    public class GetAllTopicsQuery : IRequestWrapper<IEnumerable<Topic>> { }

    public class GetAllTopicsQueryHandler : IHandlerWrapper<GetAllTopicsQuery, IEnumerable<Topic>>
    {
        private readonly ITopicService _topicService;

        public GetAllTopicsQueryHandler(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public async Task<Response<IEnumerable<Topic>>> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
        {
            return Response.Ok(await _topicService.FetchAllAsync(cancellationToken));
        }
    }
}
