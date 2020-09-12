using System;
using System.Collections.Generic;

namespace Plapp.ViewModel.Topc
{
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private Dictionary<DateTime, IDiaryEntryViewModel> _diaryEntries;
        private Dictionary<string, IDataSeriesViewModel> _dataEntries;

        public TopicViewModel()
        {
            _diaryEntries = new Dictionary<DateTime, IDiaryEntryViewModel>();
            _dataEntries = new Dictionary<string, IDataSeriesViewModel>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FirstEntryDate { get; set; }
        public DateTime LastEntryDate { get; set; }

        public IEnumerable<IDiaryEntryViewModel> DiaryEntries => _diaryEntries.Values;
        public IEnumerable<IDataSeriesViewModel> DataEntries => _dataEntries.Values;

        public void AddDataPoint(string tag, IDataPointViewModel newDataPoint)
        {
            if (!_dataEntries.ContainsKey(tag))
            {
                _dataEntries[tag] = IoC.Get<IDataSeriesViewModel>();
            }

            _dataEntries[tag].AddDataPoint(newDataPoint);
        }

        public void AddDiaryEntry(IDiaryEntryViewModel newDiaryEntry)
        {
            _diaryEntries[newDiaryEntry.Date] = newDiaryEntry;
        }

        public IDataSeriesViewModel GetDataSeries(string tag)
        {
            return _dataEntries.ContainsKey(tag) ?
                _dataEntries[tag] 
                : IoC.Get<IDataSeriesViewModel>();
        }
    }
}
