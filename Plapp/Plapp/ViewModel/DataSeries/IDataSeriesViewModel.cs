using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plapp
{
    public interface IDataSeriesViewModel : IViewModel
    {
        string Tag { get; }
        string Unit { get; }
        Icon Icon { get; set; }

        IDataPointViewModel GetDataPoint(DateTime date);
        IEnumerable<IDataPointViewModel> GetDataPointsInWindow(DateTime start, DateTime end);
        void AddDataPoint(IDataPointViewModel dataPoint);
    }
}
