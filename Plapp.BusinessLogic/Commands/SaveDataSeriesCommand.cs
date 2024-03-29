﻿using AutoMapper;
using MediatR;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic.Commands
{
    public class SaveDataSeriesCommand : IRequestWrapper
    {
        public IDataSeriesViewModel DataSeries { get; private set; }

        public SaveDataSeriesCommand(IDataSeriesViewModel dataSeries)
        {
            DataSeries = dataSeries;
        }
    }

    public class SaveDataSeriesCommandHandler : IHandlerWrapper<SaveDataSeriesCommand>
    {
        private readonly IDataSeriesService _dataSeriesService;
        private readonly IMapper _mapper;

        public SaveDataSeriesCommandHandler(
            IDataSeriesService dataSeriesService,
            IMapper mapper)
        {
            _dataSeriesService = dataSeriesService;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> Handle(SaveDataSeriesCommand request, CancellationToken cancellationToken)
        {
            var dataSeries = _mapper.Map<DataSeries>(request.DataSeries);

            await _dataSeriesService.SaveAsync(dataSeries, cancellationToken);

            request.DataSeries.Id = dataSeries.Id;

            return Response.Ok();
        }
    }
}
