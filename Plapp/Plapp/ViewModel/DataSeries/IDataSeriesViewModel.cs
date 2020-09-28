using System;
using System.Collections.Generic;

namespace Plapp
{
    public interface IDataSeriesViewModel : IViewModel
    {
        int Id { get; }

        ITagViewModel Tag { get; set; }
        IDataPointViewModel Latest { get; }

        IDataPointViewModel GetDataPoint(DateTime date);
        IEnumerable<IDataPointViewModel> GetDataPointsInWindow(DateTime start, DateTime end);
        IEnumerable<IDataPointViewModel> GetDataPoints();
        void AddDataPoint(IDataPointViewModel dataPoint);
        void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints);
    }
}
