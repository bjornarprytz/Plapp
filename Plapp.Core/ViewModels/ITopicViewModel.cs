using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface ITopicViewModel : IViewModel
    {
        bool LacksImage { get; }

        int Id { get; set; }
        string ImageUri { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        IAsyncCommand OpenCommand { get; }
        IAsyncCommand AddImageCommand { get; }
        IAsyncCommand AddDataSeriesCommand { get; }
    }
}
