using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class DiaryViewModel : BaseViewModel, IDiaryViewModel
    {
        private readonly ITopicService _topicService;
        public DiaryViewModel()
        {
            Topics = new ObservableCollection<ITopicViewModel>();

            AddTopicCommand = new CommandHandler(async () => await AddTopic());

            _topicService = ServiceLocator.Get<ITopicService>();
        }

        public bool IsBusy { get; private set; }
        public ObservableCollection<ITopicViewModel> Topics { get; private set; }
        public ICommand AddTopicCommand { get; private set; }


        private async Task AddTopic()
        {
            var newTopic = await RunCommandAsync(
                () => IsBusy,
                _topicService.Create);

            if (newTopic == null) return;

            Topics.Add(newTopic);

            Console.WriteLine($"Added topic {newTopic.Description}");

            // TODO: Navigate to TopicPage
        }
    }
}
