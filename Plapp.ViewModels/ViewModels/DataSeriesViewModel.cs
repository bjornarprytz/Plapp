using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public class DataSeriesViewModel : BaseViewModel, IDataSeriesViewModel
    {
        private readonly Dictionary<DateTime, IDataPointViewModel> _dataSeries = new Dictionary<DateTime, IDataPointViewModel>();
        private IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();
        public DataSeriesViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            LoadDataCommand = new CommandHandler(async () => await LoadData());
            AddDataPointCommand = new CommandHandler<IDataPointViewModel>(AddDataPoint);
        }

        public bool IsLoading { get; private set; }

        public int Id { get; set; }
        public string TagId { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public IEnumerable<IDataPointViewModel> DataPoints => _dataSeries.Values;

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddDataPointCommand { get; private set; }

        public void AddDataPoint(IDataPointViewModel dataPoint)
        {
            _dataSeries[dataPoint.Date] = dataPoint;
        }

        public void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints)
        {
            foreach(var dataPoint in dataPoints)
            {
                _dataSeries[dataPoint.Date] = dataPoint;
            }
        }

        private async Task LoadData()
        {
            await RunCommandAsync(
                () => IsLoading,
                async () =>
                {
                    var tag = await DataStore.FetchTagAsync(TagId);
                    Tag = tag.ToViewModel(ServiceProvider);
                });
        }
    }
}
