using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITopicViewModel : IRootViewModel
    {
        bool LacksImage { get; }

        int Id { get; set; }
        string ImageUri { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        ICommand OpenTopicCommand { get; }
        ICommand AddImageCommand { get; }
        ICommand AddDataSeriesCommand { get; }
    }
}
