using System;
using System.Collections.ObjectModel;

namespace Plapp
{
    public interface INoteViewModel : IViewModel, IData
    {
        DateTime Date { get; set; }
        string ImagePath { get; set; }
        string Header { get; set; }
        string Text { get; set; }
        ObservableCollection<IDataPointViewModel> DataPoints { get; set; }
    }
}
