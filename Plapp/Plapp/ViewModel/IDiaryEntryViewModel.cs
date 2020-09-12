using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plapp
{
    public interface IDiaryEntryViewModel : INotifyPropertyChanged
    {
        DateTime Date { get; set; }
        string ImagePath { get; set; }
        string Header { get; set; }
        string Description { get; set; }
        string Notes { get; set; }
        ICollection<IDataPointViewModel> DataPoints { get; set; }
    }
}
