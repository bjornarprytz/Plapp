using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IDataSeriesViewModel : IIOViewModel
    {
        int Id { get; set; }
        string Title { get; set; }
        ITopicViewModel Topic { get; set; }
        ITagViewModel Tag { get; set; }
        ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }

        ICommand AddDataPointCommand { get; }
    }
}
