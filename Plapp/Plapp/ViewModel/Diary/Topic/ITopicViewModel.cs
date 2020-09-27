using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp
{
    public interface ITopicViewModel : IViewModel, IData
    {
        bool IsLoadingData { get; }
        bool IsLoadingNotes { get; }

        string ImagePath { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime FirstEntryDate { get; set; }
        DateTime LastEntryDate { get; set; }

        ObservableCollection<INoteViewModel> DiaryEntries { get; }
        ObservableCollection<IDataSeriesViewModel> DataEntries { get; }

        ICommand OpenTopicCommand { get; }
        ICommand LoadDataSeriesCommand { get; }
        ICommand LoadNotesCommand { get; }

        void AddDataSeries(IDataSeriesViewModel newSeries);
        void AddNote(INoteViewModel newNote);
        void AddDataPoint(string tag, IDataPointViewModel newDataPoint);
        IDataSeriesViewModel GetDataSeries(string tag);
    }
}
