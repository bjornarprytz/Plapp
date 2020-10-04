using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IDataSeriesViewModel : IViewModel
    {
        int Id { get; }

        ITagViewModel Tag { get; set; }
        IDataPointViewModel Latest { get; }

        ICommand LoadDataCommand { get; }

        IDataPointViewModel GetDataPoint(DateTime date);
        IEnumerable<IDataPointViewModel> GetDataPointsInWindow(DateTime start, DateTime end);
        IEnumerable<IDataPointViewModel> GetDataPoints();
        void AddDataPoint(IDataPointViewModel dataPoint);
        void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints);

        ITopicViewModel Topic { get; }
    }
}
