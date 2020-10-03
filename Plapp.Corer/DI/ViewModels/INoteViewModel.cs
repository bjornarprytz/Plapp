using System;
using System.Collections.ObjectModel;

namespace Plapp.Core
{
    public interface INoteViewModel : IViewModel
    {
        int Id { get; }
        DateTime Date { get; set; }
        string ImageUri { get; set; }
        string Header { get; set; }
        string Text { get; set; }
        ObservableCollection<IDataPointViewModel> DataPoints { get; set; }
    }
}
