using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class DataSeriesViewModel : BaseViewModel, IDataSeriesViewModel
    {
        private readonly Dictionary<DateTime, IDataPointViewModel> _series = new Dictionary<DateTime, IDataPointViewModel>();
        private readonly IPlappDataStore _dataStore;

        public DataSeriesViewModel()
        {
            _dataStore = IoC.Get<IPlappDataStore>();

            LoadDataCommand = new CommandHandler(async () => await LoadData());
        }

        public bool IsLoading { get; private set; }

        public int Id { get; set; }
        public string TagId { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public IDataPointViewModel Latest => _series.Values.OrderBy(d => d.Date).FirstOrDefault();

        public ICommand LoadDataCommand { get; private set; }

        public void AddDataPoint(IDataPointViewModel dataPoint)
        {
            _series[dataPoint.Date] = dataPoint;

            OnPropertyChanged(nameof(Latest));
        }

        public void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints)
        {
            foreach(var dataPoint in dataPoints)
            {
               _series[dataPoint.Date] = dataPoint;
            }

            OnPropertyChanged(nameof(Latest));
        }

        public IDataPointViewModel GetDataPoint(DateTime date)
        {
            if (!_series.ContainsKey(date))
            {
                return null;
            }

            return _series[date];
        }

        public IEnumerable<IDataPointViewModel> GetDataPoints()
        {
            return _series.Values;
        }

        public IEnumerable<IDataPointViewModel> GetDataPointsInWindow(DateTime start, DateTime end)
        {
            return _series.Values.Where(d => d.Date > start && d.Date < end);
        }

        private async Task LoadData()
        {
            await RunCommandAsync(
                () => IsLoading,
                async () =>
                {
                    var tag = await _dataStore.FetchTagAsync(TagId);
                    Tag = tag.ToViewModel();
                });
        }
    }
}
