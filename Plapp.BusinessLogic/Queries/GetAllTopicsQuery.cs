using AutoMapper;
using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Queries
{
    public class GetAllTopicsQuery : IRequestWrapper<IEnumerable<ITopicViewModel>> { }

    public class GetAllTopicsQueryHandler : IHandlerWrapper<GetAllTopicsQuery, IEnumerable<ITopicViewModel>>
    {
        private readonly ITopicService _topicService;
        private readonly ViewModelFactory<ITopicViewModel> _createVM;
        private readonly IMapper _mapper;

        public GetAllTopicsQueryHandler(
            ITopicService topicService,
            ViewModelFactory<ITopicViewModel> viewModelFactory,
            IMapper mapper)
        {
            _topicService = topicService;
            _createVM = viewModelFactory;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ITopicViewModel>>> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
        {
            var topics = await _topicService.FetchAllAsync(cancellationToken);

            var viewModels = topics.Select(t => _mapper.Map(t, _createVM()));

            return Response.Ok(viewModels);
        }
    }
}
