using System;
using System.Collections.ObjectModel;

namespace Plapp
{
    public interface IDiaryEntryViewModel : IViewModel
    {
        DateTime Date { get; set; }
        string ImagePath { get; set; }
        string Header { get; set; }
        string Notes { get; set; }
        ObservableCollection<IDataPointViewModel> DataPoints { get; set; }
    }
}
