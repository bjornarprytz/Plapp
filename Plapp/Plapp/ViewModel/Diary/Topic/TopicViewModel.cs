﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly Dictionary<DateTime, INoteViewModel> _diaryEntries;
        private readonly Dictionary<string, IDataSeriesViewModel> _dataEntries;
        private readonly IPlappDataStore _dataStore;

        public TopicViewModel()
        {
            _diaryEntries = new Dictionary<DateTime, INoteViewModel>();
            _dataEntries = new Dictionary<string, IDataSeriesViewModel>();
            _dataStore = IoC.Get<IPlappDataStore>();

            OpenTopicCommand = new CommandHandler(async () => await OpenTopic());
            LoadDataSeriesCommand = new CommandHandler(async () => await LoadDataSeries());
            LoadNotesCommand = new CommandHandler(async () => await LoadNotes());
        }

        public int Id { get; set; }

        public ObservableCollection<INoteViewModel> DiaryEntries => new ObservableCollection<INoteViewModel>(_diaryEntries.Values);
        public ObservableCollection<IDataSeriesViewModel> DataEntries => new ObservableCollection<IDataSeriesViewModel>(_dataEntries.Values);

        public bool IsLoadingData { get; private set; }
        public bool IsLoadingNotes { get; private set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FirstEntryDate { get; set; }
        public DateTime LastEntryDate { get; set; }

        public ICommand OpenTopicCommand { get; private set; }
        public ICommand LoadDataSeriesCommand { get; private set; }
        public ICommand LoadNotesCommand { get; private set; }


        public void AddDataSeries(IDataSeriesViewModel newSeries)
        {
            _dataEntries[newSeries.Tag] = newSeries;
        }

        public void AddDataPoint(string tag, IDataPointViewModel newDataPoint)
        {
            if (!_dataEntries.ContainsKey(tag))
            {
                return;
            }

            _dataEntries[tag].AddDataPoint(newDataPoint);

            OnPropertyChanged(nameof(DataEntries));
        }

        public void AddNote(INoteViewModel newDiaryEntry)
        {
            _diaryEntries[newDiaryEntry.Date] = newDiaryEntry;

            OnPropertyChanged(nameof(DiaryEntries));
        }

        public IDataSeriesViewModel GetDataSeries(string tag)
        {
            return _dataEntries.ContainsKey(tag) ?
                _dataEntries[tag] 
                : IoC.Get<IDataSeriesViewModel>();
        }

        private async Task OpenTopic()
        {
            await NavigationHelpers.NavigateTo<ITopicViewModel>();
        }


        private async Task LoadDataSeries()
        {
            await RunCommandAsync(
                () => IsLoadingData,
                async () =>
                {
                    var dataSeries = await _dataStore.FetchDataSeries(topicId: Id);

                    foreach(var series in dataSeries)
                    {
                        AddDataSeries(series);
                    }
                });
        }

        private async Task LoadNotes()
        {
            await RunCommandAsync(
                () => IsLoadingNotes,
                async () =>
                {
                    var notes = await _dataStore.FetchNotes(topicId: Id);

                    foreach (var note in notes)
                    {
                        AddNote(note);
                    }
                });
        }

    }
}
