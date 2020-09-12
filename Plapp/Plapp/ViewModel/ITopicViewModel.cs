using System.Collections.Generic;
using System.ComponentModel;

namespace Plapp.ViewModel
{
    public interface ITopicViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }
        string Description { get; set; }
        ICollection<IDiaryEntryViewModel> DiaryEntries { get; }
        ICollection<IDataSeriesViewModel> DataCollection { get; }
    }
}
