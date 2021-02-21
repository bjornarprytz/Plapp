using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public class DataSeriesViewModel : BaseViewModel, IDataSeriesViewModel
    {
        private readonly ObservableCollection<IDataPointViewModel> _dataSeries;
        private IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();
        public DataSeriesViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _dataSeries = new ObservableCollection<IDataPointViewModel>();
            DataPoints = new ReadOnlyObservableCollection<IDataPointViewModel>(_dataSeries);

            LoadDataCommand = new CommandHandler(async () => await LoadData());
            AddDataPointCommand = new CommandHandler<IDataPointViewModel>(AddDataPoint);
        }

        public bool IsLoading { get; private set; }

        public int Id { get; set; }
        public string TagId { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddDataPointCommand { get; private set; }

        public void AddDataPoint(IDataPointViewModel dataPoint)
        {
            _dataSeries.Add(dataPoint);
        }

        public void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints)
        {
            _dataSeries.AddRange(dataPoints);
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
