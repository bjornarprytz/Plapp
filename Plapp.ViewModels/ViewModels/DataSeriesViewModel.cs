using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class DataSeriesViewModel : BaseViewModel, IDataSeriesViewModel
    {
        private readonly ObservableCollection<IDataPointViewModel> _dataPoints;
        private readonly IPrompter _prompter;
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ViewModelFactory<DataPointViewModel> _dataPointFactory;
        private readonly ILogger _logger;
        private bool hasLoadedDataSeries;

        public DataSeriesViewModel(
            IPrompter prompter,
            IDataSeriesService dataSeriesService,
            ViewModelFactory<DataPointViewModel> dataPointFactory,
            ILogger logger
            )
        {
            _prompter = prompter;
            _dataSeriesService = dataSeriesService;
            _dataPointFactory = dataPointFactory;
            _logger = logger;
            
            _dataPoints = new ObservableCollection<IDataPointViewModel>();
            DataPoints = new ReadOnlyObservableCollection<IDataPointViewModel>(_dataPoints);

            AddDataPointCommand = new AsyncCommand(AddDataPointsAsync, allowsMultipleExecutions: false);
            SaveCommand = new AsyncCommand(SaveDataAsync, allowsMultipleExecutions: false);
            RefreshCommand = new CommandHandler(RefreshData);
        }

        public bool IsLoadingData { get; private set; }
        public bool IsSavingData { get; private set; }
        public bool IsLoadingTags { get; private set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }
        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

        public ICommand AddDataPointCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private void RefreshData()
        {
            hasLoadedDataSeries = false;

            Task.Run(LoadData);
        }

        private async Task AddDataPointsAsync()
        {
            var dataPoints = await _prompter.CreateMultipleAsync<IDataPointViewModel>(
                    () => _dataPointFactory() // TODO: Make different DataPoints depending on Tag.DataType
                ); 

            if (dataPoints == default)
            {
                return;
            }

            _dataPoints.AddRange(dataPoints);
        }

        private async Task LoadData()
        {
            if (hasLoadedDataSeries)
            {
                return;
            }

            await FlagActionAsync(
                () => IsLoadingData,
                async () =>
                {
                    var dataPoints = await _dataSeriesService.FetchDataPointsAsync(Id);

                    UpdateDataPoints(dataPoints);
                    
                    hasLoadedDataSeries = true;
                });
        }

        private void UpdateDataPoints(IEnumerable<DataPoint> dataPoints)
        {
            var dataPointsToAdd = new List<IDataPointViewModel>();
            var dataPointsToRemove = new List<IDataPointViewModel>(_dataPoints);

            foreach (var _dp in dataPoints)
            {
                var existingDataPoint = _dataPoints.OfType<DataPointViewModel>().FirstOrDefault(dp => dp.Id == _dp.Id);

                if (existingDataPoint == default)
                {
                    existingDataPoint = _dataPointFactory();
                    dataPointsToAdd.Add(existingDataPoint);
                }
                else
                {
                    dataPointsToRemove.Remove(existingDataPoint);
                }
                
                existingDataPoint.Hydrate(_dp);
            }

            _dataPoints.AddRange(dataPointsToAdd);
            _dataPoints.RemoveRange(dataPointsToRemove);
        }

        private async Task SaveDataAsync()
        {
            await FlagActionAsync(
                () => IsSavingData,
                async () =>
                {
                    await _dataSeriesService.SaveAsync(this.ToModel());
                });
        }

        internal void Hydrate(DataSeries dataSeriesModel)
        {
            if (Id != 0 && Id != dataSeriesModel.Id)
                _logger.Log(LogLevel.Warning, $"Changing Id of DataSeries from {Id} to {dataSeriesModel.Id}");

            Id = dataSeriesModel.Id;
            Title = dataSeriesModel.Title;
        }
    }
}
