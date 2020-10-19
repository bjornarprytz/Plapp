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
        private readonly Dictionary<DateTime, IDataPointViewModel> DataSeries = new Dictionary<DateTime, IDataPointViewModel>();
        private IPlappDataStore DataStore => IoC.Get<IPlappDataStore>();

        public DataSeriesViewModel()
        {
            LoadDataCommand = new CommandHandler(async () => await LoadData());
            AddDataPointCommand = new CommandHandler<IDataPointViewModel>(AddDataPoint);
        }

        public bool IsLoading { get; private set; }

        public int Id { get; set; }
        public string TagId { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public IDataPointViewModel Latest => DataSeries.Values.OrderBy(d => d.Date).FirstOrDefault();

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddDataPointCommand { get; private set; }

        public void AddDataPoint(IDataPointViewModel dataPoint)
        {
            DataSeries[dataPoint.Date] = dataPoint;

            OnPropertyChanged(nameof(Latest));
        }

        public void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints)
        {
            foreach(var dataPoint in dataPoints)
            {
               DataSeries[dataPoint.Date] = dataPoint;
            }

            OnPropertyChanged(nameof(Latest));
        }

        public IDataPointViewModel GetDataPoint(DateTime date)
        {
            if (!DataSeries.ContainsKey(date))
            {
                return null;
            }

            return DataSeries[date];
        }

        public IEnumerable<IDataPointViewModel> GetDataPoints()
        {
            return DataSeries.Values;
        }

        public IEnumerable<IDataPointViewModel> GetDataPointsInWindow(DateTime start, DateTime end)
        {
            return DataSeries.Values.Where(d => d.Date > start && d.Date < end);
        }

        private async Task LoadData()
        {
            await RunCommandAsync(
                () => IsLoading,
                async () =>
                {
                    var tag = await DataStore.FetchTagAsync(TagId);
                    Tag = tag.ToViewModel();
                });
        }
    }
}
