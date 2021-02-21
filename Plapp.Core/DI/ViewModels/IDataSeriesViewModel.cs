using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IDataSeriesViewModel : IViewModel
    {
        bool IsLoadingData { get; }
        bool IsSavingData { get; }
        int Id { get; }
        string Title { get; }
        ITopicViewModel Topic { get; }
        ITagViewModel Tag { get; set; }
        ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }

        ICommand AddDataPointCommand { get; }
    }
}
