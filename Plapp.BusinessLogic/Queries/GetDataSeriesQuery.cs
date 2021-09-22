using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Plapp.Core;

namespace Plapp.BusinessLogic.Queries
{
    public class GetDataSeriesQuery : IRequestWrapper<IDataSeriesViewModel>
    {
        public int DataSeriesId { get; private set; }

        public GetDataSeriesQuery(int dataSeriesId)
        {
            DataSeriesId = dataSeriesId;
        }
    }

    public class GetDataSeriesQueryHandler : IHandlerWrapper<GetDataSeriesQuery, IDataSeriesViewModel>
    {
        private readonly IDataSeriesService _dataSeriesService;
        private readonly IViewModelFactory _vmFactory;
        private readonly IMapper _mapper;

        public GetDataSeriesQueryHandler(
            IDataSeriesService dataSeriesService,
            IViewModelFactory vmFactory,
            IMapper mapper)
        {
            _dataSeriesService = dataSeriesService;
            _vmFactory = vmFactory;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper<IDataSeriesViewModel>> Handle(GetDataSeriesQuery request, CancellationToken cancellationToken)
        {
            var dataSeries = await _dataSeriesService.FetchAsync(request.DataSeriesId, cancellationToken);

            var viewModel = _vmFactory.Create<IDataSeriesViewModel>();
 
            if (dataSeries != default)
                _mapper.Map(dataSeries, viewModel);

            return Response.Ok(viewModel);
        }
    }
}