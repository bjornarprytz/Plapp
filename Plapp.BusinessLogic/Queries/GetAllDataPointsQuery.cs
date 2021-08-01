using AutoMapper;
using Plapp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Queries
{
    public class GetAllDataPointsQuery : IRequestWrapper<IEnumerable<IDataPointViewModel>>
    {
        public int DataSeriesId { get; private set; }

        public GetAllDataPointsQuery(int dataSeriesId)
        {
            DataSeriesId = dataSeriesId;
        }
    }

    public class GetAllDataPointsQueryHandler : IHandlerWrapper<GetAllDataPointsQuery, IEnumerable<IDataPointViewModel>>
    {
        private readonly IDataSeriesService _dataSeriesService;
        private readonly IViewModelFactory _vmFactory;
        private readonly IMapper _mapper;

        public GetAllDataPointsQueryHandler(
            IDataSeriesService dataSeriesService,
            IViewModelFactory vmFactory,
            IMapper mapper)
        {
            _dataSeriesService = dataSeriesService;
            _vmFactory = vmFactory;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<IDataPointViewModel>>> Handle(GetAllDataPointsQuery request, CancellationToken cancellationToken)
        {
            var dataPoints = await _dataSeriesService.FetchDataPointsAsync(request.DataSeriesId, cancellationToken);

            var viewModels = dataPoints.Select(d => _mapper.Map(d, _vmFactory.Create<IDataPointViewModel>()));

            return Response.Ok(viewModels);
        }
    }
}
