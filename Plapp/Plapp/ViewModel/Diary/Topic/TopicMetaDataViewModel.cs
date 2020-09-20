using Plapp.Extensions;
using Plapp.ViewModel.DataSeries;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class TopicMetaDataViewModel : BaseViewModel, ITopicMetaDataViewModel
    {
        public TopicMetaDataViewModel()
        {
            OpenTopicCommand = new CommandHandler(async ()=> await OpenTopic());
        }

        public string ImagePath { get; set; } = "plant.png";
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FirstEntryDate { get; set; }
        public DateTime LastEntryDate { get; set; }

        public ICommand OpenTopicCommand { get; }

        private async Task OpenTopic()
        {
            await NavigationHelpers.NavigateTo<ITopicViewModel>(
                topic =>
                {
                    topic.MetaData = this;

                    topic.StartDataSeries(new DataSeriesViewModel { Tag = "Vann", Unit = "l" });
                    topic.StartDataSeries(new DataSeriesViewModel { Tag = "Næring", Unit = "mg" });
                    topic.AddDataPoint(
                        "Vann", new DataPointViewModel { Date = DateTime.Today, Data = 69 });
                    
                    topic.AddDataPoint(
                        "Næring", new DataPointViewModel { Date = DateTime.Today, Data = 3 });
                });
        }
    }
}
