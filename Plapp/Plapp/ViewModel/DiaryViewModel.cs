using Plapp.Core;
using System.Collections.ObjectModel;
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

            AddTopicCommand = new CommandHandler(async () => await AddTopic());

            _dataStore = IoC.Get<IPlappDataStore>();
        }

        public bool IsBusy { get; private set; }
        public ObservableCollection<ITopicViewModel> Topics { get; private set; }
        public ICommand AddTopicCommand { get; private set; }

        private async Task AddTopic()
        {
            var newTopic = await RunCommandAsync(
                () => IsBusy,
                _dataStore.CreateTopicAsync);

            if (newTopic == null) return;

            newTopic.Title = "New Title";

            await _dataStore.SaveTopicAsync(newTopic);

            Topics.Add(newTopic);
        }
    }
}
