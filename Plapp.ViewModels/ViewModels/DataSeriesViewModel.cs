using AutoMapper;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class DataSeriesViewModel : IOViewModel, IDataSeriesViewModel
    {
        private readonly ObservableCollection<IDataPointViewModel> _dataPoints;
        private readonly IPrompter _prompter;
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ViewModelFactory<IDataPointViewModel> _dataPointFactory;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DataSeriesViewModel(
            IPrompter prompter,
            IDataSeriesService dataSeriesService,
            ViewModelFactory<IDataPointViewModel> dataPointFactory,
            ILogger logger,
            IMapper mapper
            )
        {
            _prompter = prompter;
            _dataSeriesService = dataSeriesService;
            _dataPointFactory = dataPointFactory;
            _logger = logger;
            _mapper = mapper;

            _dataPoints = new ObservableCollection<IDataPointViewModel>();
            DataPoints = new ReadOnlyObservableCollection<IDataPointViewModel>(_dataPoints);

            AddDataPointCommand = new AsyncCommand(AddDataPointsAsync, allowsMultipleExecutions: false);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }

        public ICommand AddDataPointCommand { get; private set; }


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
    }
}
