using System;
using System.Collections.ObjectModel;
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
        ObservableCollection<IDataPointViewModel> DataPoints { get; set; }
    }
}
