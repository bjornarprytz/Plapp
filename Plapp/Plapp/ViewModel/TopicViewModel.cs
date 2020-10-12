using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plapp
{
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly Dictionary<DateTime, INoteViewModel> _notes;
        private readonly Dictionary<string, IDataSeriesViewModel> _dataEntries;
        private IPlappDataStore _dataStore => IoC.Get<IPlappDataStore>();

        public TopicViewModel()
        {
            _notes = new Dictionary<DateTime, INoteViewModel>();
            _dataEntries = new Dictionary<string, IDataSeriesViewModel>();

            OpenTopicCommand = new CommandHandler(async () => await OpenTopic());
            LoadDataSeriesCommand = new CommandHandler(async () => await LoadDataSeries());
            LoadNotesCommand = new CommandHandler(async () => await LoadNotes());
            SaveTopicCommand = new CommandHandler(async () => await SaveTopic());
            AddImageCommand = new CommandHandler(async () => await AddImage());
        }

        public int Id { get; set; }

        public ObservableCollection<INoteViewModel> DiaryEntries => new ObservableCollection<INoteViewModel>(_notes.Values);
        public ObservableCollection<IDataSeriesViewModel> DataEntries => new ObservableCollection<IDataSeriesViewModel>(_dataEntries.Values);

        public bool IsLoadingData { get; private set; }
        public bool IsLoadingNotes { get; private set; }
        public bool IsSavingTopic { get; private set; }
        public string ImageUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FirstEntryDate => _notes.Keys.OrderBy(d => d).FirstOrDefault();
        public DateTime LastEntryDate => _notes.Keys.OrderByDescending(d => d).FirstOrDefault();

        public ICommand OpenTopicCommand { get; private set; }
        public ICommand LoadDataSeriesCommand { get; private set; }
        public ICommand LoadNotesCommand { get; private set; }
        public ICommand SaveTopicCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }

        public void AddDataSeries(IDataSeriesViewModel newSeries)
        {
            _dataEntries[newSeries.Tag.Id] = newSeries;
            newSeries.LoadDataCommand.Execute(null);
        }

        public void AddDataSeries(IEnumerable<IDataSeriesViewModel> newSeries)
        {
            foreach(var series in newSeries)
            {
                _dataEntries[series.Tag.Id] = series;
            }

            OnPropertyChanged(nameof(DataEntries));
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

        public void AddNote(INoteViewModel newNote)
        {
            _notes[newNote.Date] = newNote;

            OnPropertyChanged(nameof(DiaryEntries));
            OnPropertyChanged(nameof(LastEntryDate));
        }

        public void AddNotes(IEnumerable<INoteViewModel> newNotes)
        {
            foreach(var note in newNotes)
            {
                _notes[note.Date] = note;
            }

            OnPropertyChanged(nameof(DiaryEntries));
            OnPropertyChanged(nameof(LastEntryDate));
        }

        public IDataSeriesViewModel GetDataSeries(string tag)
        {
            return _dataEntries.ContainsKey(tag) ?
                _dataEntries[tag] 
                : IoC.Get<IDataSeriesViewModel>();
        }

        private async Task OpenTopic()
        {
            await NavigationHelpers.NavigateTo<ITopicViewModel>(this);
        }


        private async Task LoadDataSeries()
        {
            await RunCommandAsync(
                () => IsLoadingData,
                async () =>
                {
                    var dataSeries = await _dataStore.FetchDataSeriesAsync(topicId: Id);

                    foreach(var series in dataSeries)
                    {
                        AddDataSeries(series.ToViewModel(this));
                    }
                });
        }

        private async Task LoadNotes()
        {
            await RunCommandAsync(
                () => IsLoadingNotes,
                async () =>
                {
                    var notes = await _dataStore.FetchNotesAsync(topicId: Id);

                    foreach (var note in notes)
                    {
                        AddNote(note.ToViewModel(this));
                    }
                });
        }

        private async Task SaveTopic()
        {
            await RunCommandAsync(
                () => IsSavingTopic,
                async () =>
                {
                    await _dataStore.SaveTopicAsync(this.ToModel());
                });
        }
        
        private async Task AddImage()
        {
            // TODO: Figure our navigation / popup logic

            ImageUri = "plant.png";

        }
    }
}
