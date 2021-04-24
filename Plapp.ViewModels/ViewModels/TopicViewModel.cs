using Microsoft.Extensions.Logging;
using Plapp.Core;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace Plapp.ViewModels
{
    public class TopicViewModel : PageViewModel, ITopicViewModel
    {
        private readonly ObservableCollection<IDataSeriesViewModel> _dataSeries;
        private ICamera Camera => ServiceProvider.Get<ICamera>();

        public TopicViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _dataSeries = new ObservableCollection<IDataSeriesViewModel>();
            DataSeries = new ReadOnlyObservableCollection<IDataSeriesViewModel>(_dataSeries);

            OpenTopicCommand = new AsyncCommand(OpenTopic, allowsMultipleExecutions: false);
            AddImageCommand = new AsyncCommand(AddImage, allowsMultipleExecutions: false);
            AddDataSeriesCommand = new AsyncCommand(AddDataSeriesAsync, allowsMultipleExecutions: false);
        }

        public int Id { get; private set; }
        public ReadOnlyObservableCollection<IDataSeriesViewModel> DataSeries { get; }

        public bool IsSavingTopic { get; private set; }
        
        [AlsoNotifyFor(nameof(ImageUri))]
        public bool LacksImage => string.IsNullOrEmpty(ImageUri);
        
        public string ImageUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICommand OpenTopicCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }
        public ICommand AddDataSeriesCommand { get; private set; }

        private async Task OpenTopic()
        {
            await Navigator.GoToAsync<ITopicViewModel>(this);
        }

        private async Task AddImage()
        {
            using var photoStream = await Camera.TakePhotoAsync();

            if (photoStream == null)
            {
                return;
            }

            ImageUri = await FileSystem.AppDataDirectory.SaveAsync($"{Guid.NewGuid()}.jpg", photoStream);
        }

        private async Task AddDataSeriesAsync()
        {
            // TODO: Probably don't fetch so much in this function

            var existingTags = await DataStore.FetchTagsAsync();

            var options = new List<string> { "Create new Tag" };

            options.AddRange(existingTags.Select(t => t.Key));

            var choice = await Prompter.ChooseAsync("Choose a Tag", "Cancel", null, options.ToArray());

            var tag = choice switch
            {
                "Cancel" => default,
                "Create new Tag" => await Prompter.CreateAsync<ITagViewModel>(),
                _ => existingTags.First(t => t.Key == choice).ToViewModel(ServiceProvider)
            };

            if (tag == default)
            {
                return;
            }

            var dataSeries = ServiceProvider.Get<IDataSeriesViewModel>(
                ds =>
                {
                    ds.Topic = this;
                    ds.Tag = tag;
                });

            _dataSeries.Add(dataSeries);
        }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var freshDataSeries = await DataStore.FetchDataSeriesAsync(topicId: Id);

            UpdateDataSeries(freshDataSeries);
        }

        protected override async Task AutoSaveDataAsync()
        {
            await base.AutoSaveDataAsync();
            
            var topic = this.ToModel();

            await DataStore.SaveTopicAsync(this.ToModel());

            Id = topic.Id;
        }

        internal void Hydrate(Topic topicModel)
        {
            if (Id != 0 && Id != topicModel.Id)
                Logger.Log(LogLevel.Warning, $"Changing Id of Topic from {Id} to {topicModel.Id}");

            Id = topicModel.Id;
            Title = topicModel.Title;
            Description = topicModel.Description;
            ImageUri = topicModel.ImageUri;
        }

        private void UpdateDataSeries(IEnumerable<DataSeries> dataSeries)
        {
            var dataSeriesToAdd = new List<IDataSeriesViewModel>();
            var dataSeriesToRemove = new List<IDataSeriesViewModel>(_dataSeries);

            foreach (var _ds in dataSeries)
            {
                var existingDataSeries = _dataSeries.OfType<DataSeriesViewModel>().FirstOrDefault(ds => ds.Id == _ds.Id);

                if (existingDataSeries == default)
                {
                    dataSeriesToAdd.Add(_ds.ToViewModel(ServiceProvider));
                }
                else
                {
                    existingDataSeries.Hydrate(_ds);
                    dataSeriesToRemove.Remove(existingDataSeries);
                }
            }

            _dataSeries.AddRange(dataSeriesToAdd);
            _dataSeries.RemoveRange(dataSeriesToRemove);
        }
    }
}
