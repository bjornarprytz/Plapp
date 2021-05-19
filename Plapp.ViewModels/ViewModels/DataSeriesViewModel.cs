using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public class DataSeriesViewModel : IOViewModel, IDataSeriesViewModel, IHydrate<DataSeries>
    {
        private readonly ObservableCollection<IDataPointViewModel> _dataPoints;
        private readonly IPrompter _prompter;
        private readonly IDataSeriesService _dataSeriesService;
        private readonly ViewModelFactory<IDataPointViewModel> _dataPointFactory;
        private readonly ILogger _logger;

        public DataSeriesViewModel(
            IPrompter prompter,
            IDataSeriesService dataSeriesService,
            ViewModelFactory<IDataPointViewModel> dataPointFactory,
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

            if (dataPoints == default)
            {
                return;
            }

            _dataPoints.AddRange(dataPoints);
        }

        protected override async Task AutoLoadDataAsync()
        {
            var dataPoints = await _dataSeriesService.FetchDataPointsAsync(Id);

            UpdateDataPoints(dataPoints);
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
                    existingDataPoint = _dataPointFactory() as DataPointViewModel;
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

        protected override async Task AutoSaveDataAsync()
        {
            await _dataSeriesService.SaveAsync(this.ToModel());
        }

        public void Hydrate(DataSeries domainObject)
        {
            if (Id != 0 && Id != domainObject.Id)
                _logger.Log(LogLevel.Warning, $"Changing Id of DataSeries from {Id} to {domainObject.Id}");

            Id = domainObject.Id;
            Title = domainObject.Title;
        }
    }
}
