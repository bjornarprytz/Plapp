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

        private bool hasLoadedDataSeries;

        public DataSeriesViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _dataPoints = new ObservableCollection<IDataPointViewModel>();
            DataPoints = new ReadOnlyObservableCollection<IDataPointViewModel>(_dataPoints);

            AddDataPointCommand = new AsyncCommand(AddDataPointsAsync, allowsMultipleExecutions: false);
            RefreshCommand = new CommandHandler(RefreshData);
        }

        public bool IsLoadingData { get; private set; }
        public bool IsSavingData { get; private set; }
        public bool IsLoadingTags { get; private set; }

        public int Id { get; set; }

        public string Title { get; set; }
        public string TagId { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }
        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

        public ICommand AddDataPointCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public override void OnShow()
        {
            base.OnShow();

            Task.Run(LoadData);
        }

        public override void OnUserInteractionStopped()
        {
            base.OnUserInteractionStopped();

            Task.Run(SaveData);
        }

        public void AddDataPoints(IEnumerable<IDataPointViewModel> dataPoints)
        {
            _dataPoints.AddRange(dataPoints);
        }

        public void RefreshData()
        {
            hasLoadedDataSeries = false;

            Task.Run(LoadData);
        }

        private async Task AddDataPointsAsync()
        {
            var dataPoints = new List<IDataPointViewModel> { VMFactory.Create<IDataPointViewModel>(dp => dp.Value = 69) }; // TODO: await prompter to add datapoints

            AddDataPoints(dataPoints);
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
                    var tag = await DataStore.FetchTagAsync(TagId);
                    Tag = tag.ToViewModel(ServiceProvider);

                    var dataSeries = (await DataStore.FetchDataSeriesAsync(Topic.Id, Tag.Id, Title)).FirstOrDefault();

                    _dataPoints.Clear();
                    
                    if (dataSeries != null)
                    {
                        _dataPoints.AddRange(dataSeries.DataPoints.Select(dp => dp.ToViewModel(ServiceProvider)));
                    }

                    hasLoadedDataSeries = true;
                });
        }

        private async Task SaveData()
        {
            await FlagActionAsync(
                () => IsSavingData,
                async () =>
                {
                    await DataStore.SaveDataSeriesAsync(this.ToModel());
                });
        }
    }
}
