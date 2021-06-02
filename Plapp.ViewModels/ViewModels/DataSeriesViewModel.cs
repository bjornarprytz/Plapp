using AutoMapper;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class DataSeriesViewModel : IOViewModel, IDataSeriesViewModel
    {
        private readonly ObservableCollection<IDataPointViewModel> _dataPoints;
        private readonly INavigator _navigator;
        private readonly IPrompter _prompter;
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ITagService _tagService;
        private readonly ViewModelFactory<IDataPointViewModel> _dataPointFactory;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DataSeriesViewModel(
            INavigator navigator,
            IPrompter prompter,
            IDataSeriesService dataSeriesService,
            ITagService tagService,
            ViewModelFactory<IDataPointViewModel> dataPointFactory,
            ILogger logger,
            IMapper mapper
            )
        {
            _navigator = navigator;
            _prompter = prompter;
            _dataSeriesService = dataSeriesService;
            _tagService = tagService;
            _dataPointFactory = dataPointFactory;
            _logger = logger;
            _mapper = mapper;

            _dataPoints = new ObservableCollection<IDataPointViewModel>();
            DataPoints = new ReadOnlyObservableCollection<IDataPointViewModel>(_dataPoints);

            AddDataPointCommand = new AsyncCommand(AddDataPointsAsync, allowsMultipleExecutions: false);
            OpenCommand = new AsyncCommand(OpenAsync, allowsMultipleExecutions: false);
            PickTagCommand = new AsyncCommand(PickTagAsync, allowsMultipleExecutions: false);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }

        public IAsyncCommand AddDataPointCommand { get; private set; }
        public IAsyncCommand OpenCommand { get; private set; }
        public IAsyncCommand PickTagCommand { get; private set; }

        protected override async Task AutoLoadDataAsync()
        {
            var dataPoints = await _dataSeriesService.FetchDataPointsAsync(Id);

            _dataPoints.Update(
                dataPoints,
                _mapper,
                () => _dataPointFactory(),
                (d, v) => d.Id == v.Id);
        }

        protected override async Task AutoSaveDataAsync()
        {
            await _dataSeriesService.SaveAsync(_mapper.Map<DataSeries>(this));
        }

        private async Task AddDataPointsAsync()
        {
            var dataPoints = await _prompter.CreateMultipleAsync(
                    () => _dataPointFactory() // TODO: Make different DataPoints depending on Tag.DataType
                );

            if (dataPoints == default || !dataPoints.Any())
            {
                return;
            }

            _dataPoints.AddRange(dataPoints);

            OnPropertyChanged(nameof(DataPoints));
        }

        private async Task PickTagAsync()
        {
            var existingTags = await _tagService.FetchAllAsync();

            var options = new List<string> { "Create new Tag" };

            options.AddRange(existingTags.Select(t => t.Key));

            var choice = await _prompter.ChooseAsync("Choose a Tag", "Cancel", null, options.ToArray());

            var chosenTag = choice switch
            {
                "Cancel" => default,
                "Create new Tag" => _mapper.Map<Tag>(await _prompter.CreateAsync<ITagViewModel>()),
                _ => existingTags.First(t => t.Key == choice)
            };

            if (chosenTag == default)
            {
                return;
            }

            var tag = await _tagService.SaveAsync(chosenTag);

            Tag = _mapper.Map<ITagViewModel>(chosenTag);
        }

        private async Task OpenAsync()
        {
            await _navigator.GoToAsync<IDataSeriesViewModel>(this);
        }
    }
}
