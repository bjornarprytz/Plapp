using PCLStorage;
using Plapp.Core;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private readonly ObservableCollection<IDataSeriesViewModel> _dataEntries;
        private ICamera Camera => ServiceProvider.Get<ICamera>();
        private IFileSystem FileSystem => ServiceProvider.Get<IFileSystem>();
        private IPrompter Prompter => ServiceProvider.Get<IPrompter>();

        public TopicViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _dataEntries = new ObservableCollection<IDataSeriesViewModel>();
            DataSeries = new ReadOnlyObservableCollection<IDataSeriesViewModel>(_dataEntries);

            OpenTopicCommand = new CommandHandler(async () => await OpenTopic());
            AddImageCommand = new CommandHandler(async () => await AddImage());
            AddDataSeriesCommand = new CommandHandler(async () => await AddDataSeriesAsync());
        }

        public int Id { get; set; }

        public ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        public bool IsLoadingData { get; private set; }
        public bool IsSavingTopic { get; private set; }
        public bool IsStartingCamera { get; private set; }
        
        [AlsoNotifyFor(nameof(ImageUri))]
        public bool LacksImage => string.IsNullOrEmpty(ImageUri);
        
        public string ImageUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICommand OpenTopicCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }
        public ICommand AddDataSeriesCommand { get; private set; }

        public override void OnShow()
        {
            base.OnShow();

            Task.Run(LoadDataSeries);
        }

        public override void OnUserInteractionStopped()
        {
            base.OnUserInteractionStopped();

            Task.Run(SaveTopic);
        }

        public void AddDataSeries(IDataSeriesViewModel newSeries)
        {
            var existingSeries = _dataEntries.FirstOrDefault(s => s.Tag.Id == newSeries.Tag.Id && s.Title == newSeries.Title);

            if (existingSeries == null)
            {
                _dataEntries.Add(newSeries);
            }
        }

        public void AddDataSeries(IEnumerable<IDataSeriesViewModel> newSeries)
        {
            foreach(var series in newSeries)
            {
                AddDataSeries(series);
            }
        }

        private async Task OpenTopic()
        {
            await Navigator.GoToAsync<ITopicViewModel>(this);
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
                        AddDataSeries(series.ToViewModel(this, ServiceProvider));
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
                    return await Camera.TakePhotoAsync();
                });

            if (photo == null)
            {
                return;
            }

            ImageUri = await FileSystem.SaveAsync($"{Title}.jpg", photo);
        }

        private async Task AddDataSeriesAsync()
        {
            var options = new List<string> { "Create new Tag" };

            options.AddRange((await DataStore.FetchTagsAsync()).Select(t => t.Id));

            var choice = await Prompter.ChooseAsync("Choose a Tag", "Cancel", null, options.ToArray());


            var tag = choice == "Create new Tag"
            ? await Prompter.CreateAsync<ITagViewModel>()
            : (await DataStore.FetchTagAsync(choice))?.ToViewModel(ServiceProvider);

            if (tag == null)
            {
                return;
            }

            var dataSeries = VMFactory.Create<IDataSeriesViewModel>(
                ds =>
                {
                    ds.Topic = this;
                    ds.Tag = tag;
                });

            AddDataSeries(dataSeries);
        }
    }
}
