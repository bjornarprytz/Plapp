using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public class DataPointViewModel : BaseViewModel, IDataPointViewModel, IHydrate<DataPoint>
    {
        private readonly ILogger _logger;

        public DataPointViewModel(ILogger logger)
        {
            _logger = logger;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long Value { get; set; }

        public void Hydrate(DataPoint domainObject)
        {
            if (Id != 0 && Id != domainObject.Id)
                _logger.Log(LogLevel.Warning, $"Changing Id of DataPoint from {Id} to {domainObject.Id}");

            Id = domainObject.Id;
            Date = domainObject.Date;
            Value = domainObject.Value;
        }
    }
}
