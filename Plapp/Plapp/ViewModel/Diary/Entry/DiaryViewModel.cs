using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class DiaryViewModel : BaseViewModel
    {
        private readonly ITopicViewModelFactory _topicViewModelFactory;
        public DiaryViewModel(ITopicViewModelFactory topicViewModelFactory)
        {
            _topicViewModelFactory = topicViewModelFactory;
            AddTopicCommand = new CommandHandler(async () => await AddTopic());
        }

        public bool IsBusy { get; private set; }
        public ObservableCollection<ITopicViewModel> Topics { get; private set; }
        public ICommand AddTopicCommand { get; set; }


        private async Task AddTopic()
        {
            var newTopic = await RunCommandAsync(
                () => IsBusy,
                _topicViewModelFactory.Create);

            Topics.Add(newTopic);

            // TODO: Navigate to TopicPage
        }
    }
}
