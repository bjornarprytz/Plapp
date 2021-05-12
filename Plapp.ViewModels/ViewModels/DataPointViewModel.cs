using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public class DataPointViewModel : BaseViewModel, IDataPointViewModel
    {
        private readonly ILogger _logger;

        public DataPointViewModel(ILogger logger)
        {
            _logger = logger;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long Value { get; set; }

        internal void Hydrate(DataPoint dataPoint)
        {
            if (Id != 0 && Id != dataPoint.Id)
                _logger.Log(LogLevel.Warning, $"Changing Id of DataPoint from {Id} to {dataPoint.Id}");

            Id = dataPoint.Id;
            Date = dataPoint.Date;
            Value = dataPoint.Value;
        }
    }
}
