using System;
using System.ComponentModel;

namespace Plapp
{
    public interface IDataSeriesViewModel : INotifyPropertyChanged
    {
        string Tag { get; }
        string Unit { get; }
        IDataPointViewModel GetDataPoint(DateTime date);
        Icon Icon { get; set; }
    }
}
