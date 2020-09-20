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
            Topics = new ObservableCollection<ITopicMetaDataViewModel>();

            Topics.Add(new TopicMetaDataViewModel { Title = "A title", Description = "some description", LastEntryDate = DateTime.Now });

            AddTopicCommand = new CommandHandler(async () => await AddTopic());
            
            _topicService = IoC.Get<ITopicService>();
        }

        public bool IsBusy { get; private set; }
        public ObservableCollection<ITopicMetaDataViewModel> Topics { get; private set; }
        public ICommand AddTopicCommand { get; private set; }

        private async Task AddTopic()
        {
            var newTopic = await RunCommandAsync(
                () => IsBusy,
                _topicService.CreateMetaData);

            if (newTopic == null) return;

            newTopic.Title = "New Title";

            Topics.Add(newTopic);
        }
    }
}
