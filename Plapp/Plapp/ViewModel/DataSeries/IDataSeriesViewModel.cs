using System;
using System.Collections.Generic;

namespace Plapp
{
    public interface IDataSeriesViewModel : IViewModel, IData
    {
        string Tag { get; set; }
        string Unit { get; set; }
        Icon Icon { get; set; }

        IDataPointViewModel Latest { get; }

        IDataPointViewModel GetDataPoint(DateTime date);
        IEnumerable<IDataPointViewModel> GetDataPointsInWindow(DateTime start, DateTime end);
        void AddDataPoint(IDataPointViewModel dataPoint);
    }
}
