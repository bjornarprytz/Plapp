using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plapp
{
    public class DataSeriesViewModel : BaseViewModel, IDataSeriesViewModel
    {
        private readonly Dictionary<DateTime, IDataPointViewModel> _series = new Dictionary<DateTime, IDataPointViewModel>();

        public int Id { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public IDataPointViewModel Latest => _series.Values.OrderBy(d => d.Date).FirstOrDefault();


        public void AddDataPoint(IDataPointViewModel dataPoint)
        {
            _series[dataPoint.Date] = dataPoint;

            OnPropertyChanged(nameof(Latest));
        }

        public void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints)
        {
            foreach(var dataPoint in dataPoints)
            {
               _series[dataPoint.Date] = dataPoint;
            }

            OnPropertyChanged(nameof(Latest));
        }

        public IDataPointViewModel GetDataPoint(DateTime date)
        {
            if (!_series.ContainsKey(date))
            {
                return null;
            }

            return _series[date];
        }

        public IEnumerable<IDataPointViewModel> GetDataPoints()
        {
            return _series.Values;
        }

        public IEnumerable<IDataPointViewModel> GetDataPointsInWindow(DateTime start, DateTime end)
        {
            return _series.Values.Where(d => d.Date > start && d.Date < end);
        }
    }
}
