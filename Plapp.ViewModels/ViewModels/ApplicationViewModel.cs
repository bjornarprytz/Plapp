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
        private readonly ObservableCollection<ITopicViewModel> _topics;

        private bool topicsLoaded = false;

        private INavigator Navigator => ServiceProvider.Get<INavigator>();
        private IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();

        public ApplicationViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _topics = new ObservableCollection<ITopicViewModel>();
            Topics = new ReadOnlyObservableCollection<ITopicViewModel>(_topics);

            AddTopicCommand = new CommandHandler(async () => await AddTopic());
            DeleteTopicCommand = new CommandHandler<ITopicViewModel>(DeleteTopic);
        }

        public bool IsLoadingTopics { get; private set; }

        public ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        
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

                    _topics.Clear();
                    _topics.AddRange(topics.Select(t => t.ToViewModel(ServiceProvider)));

                    topicsLoaded = true;
            });
        }

        private async Task AddTopic()
        {
            var newTopic = new TopicViewModel(ServiceProvider);

            _topics.Add(newTopic);

            _ = DataStore.SaveTopicAsync(newTopic.ToModel());

            await Navigator.GoToAsync<ITopicViewModel>(newTopic);
        }

        private void DeleteTopic(ITopicViewModel topic)
        {
            _topics.Remove(topic);
            
            Task.Run(() => DataStore.DeleteTopicAsync(topic.ToModel()));
        }
    }
}
