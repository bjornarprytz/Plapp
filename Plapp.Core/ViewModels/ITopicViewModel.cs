using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITopicViewModel : IViewModel
    {
        int Id { get; set; }
        string ImageUri { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        ICommand DeleteCommand { get; }
        ICommand OpenCommand { get; }
        ICommand AddImageCommand { get; }
        ICommand AddDataSeriesCommand { get; }
    }
}
