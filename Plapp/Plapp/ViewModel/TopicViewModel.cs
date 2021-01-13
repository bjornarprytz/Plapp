using Plapp.Core;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly Dictionary<string, IDataSeriesViewModel> _dataEntries;
        private IPlappDataStore DataStore => IoC.Get<IPlappDataStore>();

        public TopicViewModel()
        {
            _dataEntries = new Dictionary<string, IDataSeriesViewModel>();

            OpenTopicCommand = new CommandHandler(async () => await OpenTopic());
            LoadDataSeriesCommand = new CommandHandler(async () => await LoadDataSeries());
            SaveTopicCommand = new CommandHandler(async () => await SaveTopic());
            AddImageCommand = new CommandHandler(async () => await AddImage());
            AddTagCommand = new CommandHandler(async () => await AddTag());
        }

        public int Id { get; set; }

        public ObservableCollection<IDataSeriesViewModel> DataEntries => new ObservableCollection<IDataSeriesViewModel>(_dataEntries.Values);

        public bool IsLoadingData { get; private set; }
        public bool IsSavingTopic { get; private set; }
        public bool IsStartingCamera { get; private set; }
        
        [AlsoNotifyFor(nameof(ImageUri))]
        public bool LacksImage => string.IsNullOrEmpty(ImageUri);
        
        public string ImageUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICommand OpenTopicCommand { get; private set; }
        public ICommand LoadDataSeriesCommand { get; private set; }
        public ICommand SaveTopicCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }
        public ICommand AddTagCommand { get; private set; }

        public void AddDataSeries(IDataSeriesViewModel newSeries)
        {
            _dataEntries[newSeries.Tag.Id] = newSeries;

            OnPropertyChanged(nameof(DataEntries));
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
                    var dataSeries = await DataStore.FetchDataSeriesAsync(topicId: Id);

                    foreach(var series in dataSeries)
                    {
                        AddDataSeries(series.ToViewModel(this));
                    }
                });
        }

        private async Task SaveTopic()
        {
            await RunCommandAsync(
                () => IsSavingTopic,
                async () =>
                {
                    await DataStore.SaveTopicAsync(this.ToModel());
                });
        }
        
        private async Task AddImage()
        {
            var photo = await RunCommandAsync(
                () => IsStartingCamera,
                async () =>
                {
                    return await IoC.Get<ICamera>().TakePhotoAsync();
                });


            if (photo == null)
            {
                return;
            }    

            ImageUri = await DataStore.SaveFileAsync($"{Title}.jpg", photo);

            await SaveTopic();
        }

        private async Task AddTag()
        {
            var tag = new Tag { Id = "Vann", Unit = "L" }.ToViewModel();

            AddDataSeries(new DataSeriesViewModel { Topic = this, Tag = tag });
        }
    }
}
