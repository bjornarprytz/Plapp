using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plapp.ViewModel
{
    public interface IGraphViewModel : INotifyPropertyChanged
    {
        string Header { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        ICollection<IDataSeriesViewModel> DataSeries { get; set; }
    }
}
