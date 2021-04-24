using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;

namespace Plapp.ViewModels
{
    public class DataPointViewModel : BaseViewModel, IDataPointViewModel
    {
        public DataPointViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long Value { get; set; }

        internal void Hydrate(DataPoint dataPoint)
        {
            if (Id != 0 && Id != dataPoint.Id)
                Logger.Log(LogLevel.Warning, $"Changing Id of DataPoint from {Id} to {dataPoint.Id}");

            Id = dataPoint.Id;
            Date = dataPoint.Date;
            Value = dataPoint.Value;
        }
    }
}
