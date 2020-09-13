using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp
{
    public interface ITopicViewModel : IViewModel
    {
        string Title { get; set; }
        string Description { get; set; }
        DateTime FirstEntryDate { get; set; }
        DateTime LastEntryDate { get; set; }
        ObservableCollection<IDiaryEntryViewModel> DiaryEntries { get; }
        ObservableCollection<IDataSeriesViewModel> DataEntries { get; }

        ICommand DeleteTopicCommand { get; }

        void AddDiaryEntry(IDiaryEntryViewModel newDiaryEntry);
        void AddDataPoint(string tag, IDataPointViewModel newDataPoint);
        IDataSeriesViewModel GetDataSeries(string tag);
    }
}
