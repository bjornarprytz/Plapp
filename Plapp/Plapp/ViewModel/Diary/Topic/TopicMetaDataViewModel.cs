using Plapp.Extensions;
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

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FirstEntryDate { get; set; }
        public DateTime LastEntryDate { get; set; }

        public ICommand OpenTopicCommand { get; }

        private async Task OpenTopic()
        {
            await NavigationHelpers.NavigateTo<ITopicViewModel>(
                topic => topic.MetaData = this);
        }
    }
}
