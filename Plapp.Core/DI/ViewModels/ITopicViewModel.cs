using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITopicViewModel : IViewModel
    {
        bool IsLoadingData { get; }
        bool IsLoadingNotes { get; }
        bool IsSavingTopic { get; }
        bool LacksImage { get; }

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
        ICommand SaveTopicCommand { get; }
        ICommand AddImageCommand { get; }
        ICommand AddTagCommand { get; }


        void AddDataSeries(IDataSeriesViewModel newSeries);
        void AddDataSeries(IEnumerable<IDataSeriesViewModel> newSeries);
        void AddNote(INoteViewModel newNote);
        void AddNotes(IEnumerable<INoteViewModel> newNotes);
        void AddDataPoint(string tag, IDataPointViewModel newDataPoint);
        IDataSeriesViewModel GetDataSeries(string tag);
    }
}
