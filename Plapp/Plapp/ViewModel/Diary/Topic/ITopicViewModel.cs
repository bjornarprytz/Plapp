using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plapp
{
    public interface ITopicViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }
        string Description { get; set; }
        DateTime FirstEntryDate { get; set; }
        DateTime LastEntryDate { get; set; }
        IEnumerable<IDiaryEntryViewModel> DiaryEntries { get; }
        IEnumerable<IDataSeriesViewModel> DataEntries { get; }
        void AddDiaryEntry(IDiaryEntryViewModel newDiaryEntry);
        void AddDataPoint(string tag, IDataPointViewModel newDiaryEntry);
        IDataSeriesViewModel GetDataSeries(string tag);
    }
}
