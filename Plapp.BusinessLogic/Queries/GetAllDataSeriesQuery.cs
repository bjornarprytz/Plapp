using AutoMapper;
using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Queries
{
    public class GetAllDataSeriesQuery : IRequestWrapper<IEnumerable<IDataSeriesViewModel>>
    {
        public int? TopicId { get; private set; }
        public int? TagId { get; private set; }

        public GetAllDataSeriesQuery(int? topicId = null, int? tagId = null)
        {
            TopicId = topicId;
            TagId = tagId;
        }
    }

    public class GetAllDataSeriesQueryHandler : IHandlerWrapper<GetAllDataSeriesQuery, IEnumerable<IDataSeriesViewModel>>
    {
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ViewModelFactory<IDataSeriesViewModel> _viewModelFactory;
        private readonly IMapper _mapper;

        public GetAllDataSeriesQueryHandler(
            IDataSeriesService dataSeriesService,
            ViewModelFactory<IDataSeriesViewModel> viewModelFactory,
            IMapper mapper)
        {
            _dataSeriesService = dataSeriesService;
            _viewModelFactory = viewModelFactory;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<IDataSeriesViewModel>>> Handle(GetAllDataSeriesQuery request, CancellationToken cancellationToken)
        {
            var dataSeries = await _dataSeriesService.FetchAllAsync(request.TopicId, request.TagId, cancellationToken);

            var viewModels = dataSeries.Select(ds => _mapper.Map(ds, _viewModelFactory()));

            return Response.Ok(viewModels);
        }
    }
}
