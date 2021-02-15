using Plapp.Core;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class TopicViewModel : BaseViewModel, ITopicViewModel
    {
        private IPlappDataStore DataStore => IoC.Get<IPlappDataStore>();

        public TopicViewModel()
        {
            OpenTopicCommand = new CommandHandler(async () => await OpenTopic());
            AddImageCommand = new CommandHandler(async () => await AddImage());
            AddDataSeriesCommand = new CommandHandler(AddTag);
        }

        public int Id { get; set; }

        public ObservableCollection<IDataSeriesViewModel> DataEntries { get; private set; } = new ObservableCollection<IDataSeriesViewModel>();

        public bool IsLoadingData { get; private set; }
        public bool IsSavingTopic { get; private set; }
        public bool IsStartingCamera { get; private set; }
        
        [AlsoNotifyFor(nameof(ImageUri))]
        public bool LacksImage => string.IsNullOrEmpty(ImageUri);
        
        public string ImageUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICommand OpenTopicCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }
        public ICommand AddDataSeriesCommand { get; private set; }

        public override void OnShow()
        {
            base.OnShow();

            Task.Run(LoadDataSeries);
        }

        public override void OnUserInteractionStopped()
        {
            base.OnUserInteractionStopped();

            Task.Run(SaveTopic);
        }

        public void AddDataSeries(IDataSeriesViewModel newSeries)
        {
            var existingSeries = DataEntries.FirstOrDefault(s => s.Tag.Id == newSeries.Tag.Id);

            if (existingSeries != null)
            {
                existingSeries.AddDataPoints(newSeries.GetDataPoints());
            }
            else
            {
                DataEntries.Add(newSeries);
            }
        }

        public void AddDataSeries(IEnumerable<IDataSeriesViewModel> newSeries)
        {
            foreach(var series in newSeries)
            {
                AddDataSeries(series);
            }
        }

        private async Task OpenTopic()
        {
            await NavigationHelpers.NavigateTo<ITopicViewModel>(this);
        }


        private async Task LoadDataSeries()
        {
            await RunCommandAsync(
                () => IsLoadingData,
                async () =>
                {
                    var dataSeries = await DataStore.FetchDataSeriesAsync(topicId: Id);

                    foreach(var series in dataSeries)
                    {
                        AddDataSeries(series.ToViewModel(this));
                    }
                });
        }

        private async Task SaveTopic()
        {
            await RunCommandAsync(
                () => IsSavingTopic,
                async () =>
                {
                    await DataStore.SaveTopicAsync(this.ToModel());
                });
        }
        
        private async Task AddImage()
        {
            var photo = await RunCommandAsync(
                () => IsStartingCamera,
                async () =>
                {
                    return await IoC.Get<ICamera>().TakePhotoAsync();
                });

            if (photo == null)
            {
                return;
            }

            ImageUri = await FileHelpers.SaveAsync($"{Title}.jpg", photo);
        }

        private void AddTag()
        {
            // TODO: Replace this dummy code

            var tag = new Tag { Id = "Vann", Unit = "L" }.ToViewModel();

            AddDataSeries(new DataSeriesViewModel { Topic = this, Tag = tag });
        }
    }
}
