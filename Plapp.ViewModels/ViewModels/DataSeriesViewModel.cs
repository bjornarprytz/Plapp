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
        public string TagKey { get; set; }

        public ITagViewModel Tag { get; set; }
        public ITopicViewModel Topic { get; set; }
        public ReadOnlyObservableCollection<IDataPointViewModel> DataPoints { get; }
        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

        public ICommand AddDataPointCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }


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
            var dataPoints = new List<IDataPointViewModel> { ServiceProvider.Get<IDataPointViewModel>(dp => dp.Value = 69) }; // TODO: await prompter to add datapoints

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
                    if (Tag == null)
                    {
                        Tag = (await DataStore.FetchTagAsync(TagKey)).ToViewModel(ServiceProvider);
                    }

                    var dataPoints = await DataStore.FetchDataPointsAsync(Id);

                    _dataPoints.Clear();
                    
                    _dataPoints.AddRange(dataPoints.Select(dp => dp.ToViewModel(ServiceProvider)));
                    
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
