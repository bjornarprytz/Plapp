using Plapp.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        private IPlappDataStore DataStore => IoC.Get<IPlappDataStore>();

        private bool topicsLoaded = false;

        public ApplicationViewModel()
        {
            AddTopicCommand = new CommandHandler(async () => await AddTopic());
            DeleteTopicCommand = new CommandHandler<ITopicViewModel>(DeleteTopic);
        }

        public bool IsLoadingTopics { get; private set; }

        public ObservableCollection<ITopicViewModel> Topics { get; private set; } = new ObservableCollection<ITopicViewModel>();
        
        public ICommand AddTopicCommand { get; private set; }
        public ICommand DeleteTopicCommand { get; private set; }

        public override async void OnShow()
        {
            base.OnShow();
            await LoadTopics();
        }

        private async Task LoadTopics()
        {
            if (topicsLoaded)
            {
                return;
            }

            await RunCommandAsync(
                () => IsLoadingTopics,
                async () => {
                    var topics = await DataStore.FetchTopicsAsync();
                    Topics = new ObservableCollection<ITopicViewModel>(
                        topics.Select(t => t.ToViewModel()));

                    topicsLoaded = true;
            });
        }

        private async Task AddTopic()
        {
            var newTopic = new TopicViewModel();

            Topics.Add(newTopic);

            _ = DataStore.SaveTopicAsync(newTopic.ToModel());

            await NavigationHelpers.NavigateTo<ITopicViewModel>(newTopic);
        }

        private void DeleteTopic(ITopicViewModel topic)
        {
            Topics.Remove(topic);
            
            _ = DataStore.DeleteTopicAsync(topic.ToModel());
        }
    }
}
