using AutoMapper;
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
    public class TopicViewModel : IOViewModel, ITopicViewModel
    {
        private readonly ObservableCollection<IDataSeriesViewModel> _dataSeries;
        private readonly ICamera _camera;
        private readonly INavigator _navigator;
        private readonly ITagService _tagService;
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ITopicService _topicService;
        private readonly IPrompter _prompter;
        private readonly ViewModelFactory<IDataSeriesViewModel> _dataSeriesFactory;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TopicViewModel(
            ICamera camera,
            INavigator navigator,
            ITagService tagService,
            IDataSeriesService dataSeriesService,
            ITopicService topicService,
            IPrompter prompter,
            ViewModelFactory<IDataSeriesViewModel> dataSeriesFactory,
            ILogger logger,
            IMapper mapper
            )
        {
            _camera = camera;
            _navigator = navigator;
            _tagService = tagService;
            _dataSeriesService = dataSeriesService;
            _topicService = topicService;
            _prompter = prompter;
            _dataSeriesFactory = dataSeriesFactory;
            _logger = logger;
            _mapper = mapper;

            _dataSeries = new ObservableCollection<IDataSeriesViewModel>();
            DataSeries = new ReadOnlyObservableCollection<IDataSeriesViewModel>(_dataSeries);

            OpenTopicCommand = new AsyncCommand(OpenTopic, allowsMultipleExecutions: false);
            AddImageCommand = new AsyncCommand(AddImage, allowsMultipleExecutions: false);
            AddDataSeriesCommand = new AsyncCommand(AddDataSeriesAsync, allowsMultipleExecutions: false);
        }

        public int Id { get; set; }
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

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var freshDataSeries = await _dataSeriesService.FetchAllAsync(topicId: Id);

            _dataSeries.Update(
                freshDataSeries,
                _mapper,
                () => _dataSeriesFactory(),
                (d, v) => d.Id == v.Id);
        }

        protected override async Task AutoSaveDataAsync()
        {
            await base.AutoSaveDataAsync();

            var topic = await _topicService.SaveAsync(_mapper.Map<Topic>(this));

            Id = topic.Id;
        }

        private async Task OpenTopic()
        {
            await _navigator.GoToAsync<ITopicViewModel>(this);
        }

        private async Task AddImage()
        {
            using var photoStream = await _camera.TakePhotoAsync();

            if (photoStream == null)
            {
                return;
            }

            ImageUri = await FileSystem.AppDataDirectory.SaveAsync($"{Guid.NewGuid()}.jpg", photoStream);
        }

        private async Task AddDataSeriesAsync()
        {
            var existingTags = await _tagService.FetchAllAsync();

            var options = new List<string> { "Create new Tag" };

            options.AddRange(existingTags.Select(t => t.Key));

            var choice = await _prompter.ChooseAsync("Choose a Tag", "Cancel", null, options.ToArray());

            var tag = choice switch
            {
                "Cancel" => default,
                "Create new Tag" => await _prompter.CreateAsync<ITagViewModel>(),
                _ => _mapper.Map<ITagViewModel>(existingTags.First(t => t.Key == choice))
            };

            if (tag == default)
            {
                return;
            }

            var tagData = await _tagService.SaveAsync(_mapper.Map<Tag>(tag));

            tag.Id = tagData.Id;

            var dataSeries = _dataSeriesFactory();

            dataSeries.Topic = this;
            dataSeries.Tag = tag;

            _dataSeries.Add(dataSeries);
        }
    }
}
