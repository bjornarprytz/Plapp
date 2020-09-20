using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp
{
    public interface ITopicViewModel : IViewModel
    {
        ITopicMetaDataViewModel MetaData { get; set; }
        ObservableCollection<IDiaryEntryViewModel> DiaryEntries { get; }
        ObservableCollection<IDataSeriesViewModel> DataEntries { get; }

        ICommand DeleteTopicCommand { get; }

        void AddDiaryEntry(IDiaryEntryViewModel newDiaryEntry);
        void AddDataPoint(string tag, IDataPointViewModel newDataPoint);
        IDataSeriesViewModel GetDataSeries(string tag);
    }
}
