using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.ViewModel.Topc
{
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly Dictionary<DateTime, IDiaryEntryViewModel> _diaryEntries;
        private readonly Dictionary<string, IDataSeriesViewModel> _dataEntries;

        public TopicViewModel()
        {
            _diaryEntries = new Dictionary<DateTime, IDiaryEntryViewModel>();
            _dataEntries = new Dictionary<string, IDataSeriesViewModel>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FirstEntryDate { get; set; }
        public DateTime LastEntryDate { get; set; }

        public ObservableCollection<IDiaryEntryViewModel> DiaryEntries => new ObservableCollection<IDiaryEntryViewModel>(_diaryEntries.Values);
        public ObservableCollection<IDataSeriesViewModel> DataEntries => new ObservableCollection<IDataSeriesViewModel>(_dataEntries.Values);

        public ICommand DeleteTopicCommand { get; private set; }

        public void AddDataPoint(string tag, IDataPointViewModel newDataPoint)
        {
            if (!_dataEntries.ContainsKey(tag))
            {
                _dataEntries[tag] = ViewModelLocator.Get<IDataSeriesViewModel>();
            }

            _dataEntries[tag].AddDataPoint(newDataPoint);

            OnPropertyChanged(nameof(DataEntries));
        }

        public void AddDiaryEntry(IDiaryEntryViewModel newDiaryEntry)
        {
            _diaryEntries[newDiaryEntry.Date] = newDiaryEntry;

            OnPropertyChanged(nameof(DiaryEntries));
        }

        public IDataSeriesViewModel GetDataSeries(string tag)
        {
            return _dataEntries.ContainsKey(tag) ?
                _dataEntries[tag] 
                : ViewModelLocator.Get<IDataSeriesViewModel>();
        }
    }
}
