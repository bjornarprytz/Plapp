using Plapp.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class DiaryViewModel : BaseViewModel, IDiaryViewModel
    {
        private readonly IPlappDataStore _dataStore;

        public DiaryViewModel()
        {
            Topics = new ObservableCollection<ITopicViewModel>
            {
                new TopicViewModel { Title = "A title", Description = "some description" }
            };

            AddTopicCommand = new CommandHandler(AddTopic);
            SaveTopicsCommand = new CommandHandler(async () => await SaveTopics());
            DeleteTopicCommand = new CommandHandler<ITopicViewModel>(async (topic) => await DeleteTopic(topic));

            _dataStore = IoC.Get<IPlappDataStore>();
        }

        public bool IsBusy { get; private set; }
        public ObservableCollection<ITopicViewModel> Topics { get; private set; }
        public ICommand AddTopicCommand { get; private set; }

        public ICommand SaveTopicsCommand { get; private set; }

        public ICommand DeleteTopicCommand { get; private set; }

        private void AddTopic()
        {
            var newTopic = new TopicViewModel 
            { 
                Title = "New Title"
            };

            Topics.Add(newTopic);
        }

        private async Task SaveTopics()
        {
            await RunCommandAsync(
                () => IsBusy,
                async () =>
                {
                    await _dataStore.SaveTopicsAsync(Topics.Select(t => t.ToModel()));
                });
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            await RunCommandAsync(
                () => IsBusy,
                async () =>
                {
                    await _dataStore.DeleteTopicAsync(topic.ToModel());
                });
        }
    }
}
