using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITopicViewModel : IViewModel
    {
        bool IsLoadingData { get; }
        bool IsLoadingNotes { get; }

        int Id { get; }
        string ImageUri { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        DateTime FirstEntryDate { get; }
        DateTime LastEntryDate { get; }
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
