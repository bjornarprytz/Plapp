using Plapp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        private bool topicsLoaded = false;

        private INavigator Navigator => ServiceProvider.Get<INavigator>();
        private IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();

        public ApplicationViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddTopicCommand = new CommandHandler(async () => await AddTopic());
            DeleteTopicCommand = new CommandHandler<ITopicViewModel>(DeleteTopic);
        }

        public bool IsLoadingTopics { get; private set; }

        public ObservableCollection<ITopicViewModel> Topics { get; private set; } = new ObservableCollection<ITopicViewModel>();
        
        public ICommand AddTopicCommand { get; private set; }
        public ICommand DeleteTopicCommand { get; private set; }

        public override void OnShow()
        {
            base.OnShow();
            Task.Run(LoadTopics);
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
                        topics.Select(t => t.ToViewModel(ServiceProvider)));

                    topicsLoaded = true;
            });
        }

        private async Task AddTopic()
        {
            var newTopic = new TopicViewModel(ServiceProvider);

            Topics.Add(newTopic);

            _ = DataStore.SaveTopicAsync(newTopic.ToModel());

            await Navigator.GoToAsync<ITopicViewModel>(newTopic);
        }

        private void DeleteTopic(ITopicViewModel topic)
        {
            Topics.Remove(topic);
            
            _ = DataStore.DeleteTopicAsync(topic.ToModel());
        }
    }
}
