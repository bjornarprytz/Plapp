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

        public ApplicationViewModel()
        {
            AddTopicCommand = new CommandHandler(async () => await AddTopic());
            DeleteTopicCommand = new CommandHandler<ITopicViewModel>(async (topic) => await DeleteTopic(topic));
            LoadTopicsCommand = new CommandHandler(async () => await LoadTopics());
        }

        public bool IsBusy { get; private set; }

        public ObservableCollection<ITopicViewModel> Topics { get; private set; } = new ObservableCollection<ITopicViewModel>();
        
        public ICommand AddTopicCommand { get; private set; }
        public ICommand DeleteTopicCommand { get; private set; }
        public ICommand LoadTopicsCommand { get; private set; }

        private async Task LoadTopics()
        {
            await RunCommandAsync(
                () => IsBusy,
                async () => {
                    var topics = await DataStore.FetchTopicsAsync();
                    Topics = new ObservableCollection<ITopicViewModel>(
                        topics.Select(t => t.ToViewModel()));
            });
        }

        private async Task AddTopic()
        {
            var newTopic = new TopicViewModel();

            await RunCommandAsync(
                () => IsBusy,
                async () =>
                {
                    await DataStore.SaveTopicAsync(newTopic.ToModel());
                });

            Topics.Add(newTopic);

            await NavigationHelpers.NavigateTo<ITopicViewModel>(newTopic);
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            await RunCommandAsync(
                () => IsBusy,
                async () =>
                {
                    await DataStore.DeleteTopicAsync(topic.ToModel());
                    Topics.Remove(topic);
                });
        }
    }
}
