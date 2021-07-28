using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface ITopicViewModel : IIOViewModel
    {
        bool LacksImage { get; }

        int Id { get; set; }
        string ImageUri { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        ObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        IAsyncCommand OpenCommand { get; }
        IAsyncCommand AddImageCommand { get; }
        IAsyncCommand AddDataSeriesCommand { get; }
    }
}
