using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IDataSeriesViewModel : IViewModel
    {
        int Id { get; set; }
        string Title { get; set; }
        ITagViewModel Tag { get; set; }
        ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }

        ICommand OpenCommand { get; }
        ICommand AddDataPointCommand { get; }
        ICommand PickTagCommand { get; }
    }
}
