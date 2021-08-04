using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface IDataSeriesViewModel : IIOViewModel
    {
        int Id { get; set; }
        string Title { get; set; }
        ITagViewModel Tag { get; set; }
        ObservableCollection<IDataPointViewModel> DataPoints { get; }

        IAsyncCommand OpenCommand { get; }
        IAsyncCommand AddDataPointCommand { get; }
        IAsyncCommand PickTagCommand { get; }
    }
}
