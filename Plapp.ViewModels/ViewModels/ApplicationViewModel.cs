using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : PageViewModel, IApplicationViewModel
    {
        private readonly ObservableCollection<ITopicViewModel> _topics;
        private readonly INavigator _navigator;
        private readonly ViewModelFactory<TopicViewModel> _topicFactory;
        private readonly ITopicService _topicService;

        public ApplicationViewModel(
            INavigator navigator,
            ViewModelFactory<TopicViewModel> topicFactory, 
            ITopicService topicService
            )
        {
            _navigator = navigator;
            _topicFactory = topicFactory;
            _topicService = topicService;

            _topics = new ObservableCollection<ITopicViewModel>();
            Topics = new ReadOnlyObservableCollection<ITopicViewModel>(_topics);

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new Command<ITopicViewModel>(DeleteTopic);
        }

        public ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        
        public ICommand AddTopicCommand { get; private set; }
        public ICommand DeleteTopicCommand { get; private set; }

        private async Task AddTopic()
        {
            var newTopic = _topicFactory();

            _topics.Add(newTopic);

            _ = _topicService.SaveAsync(newTopic.ToModel());

            await _navigator.GoToAsync(newTopic);
        }

        private void DeleteTopic(ITopicViewModel topic)
        {
            _topics.Remove(topic);
            
            Task.Run(() => _topicService.DeleteAsync(topic.ToModel()));
        }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var freshTopics = await _topicService.FetchAllAsync();

            UpdateTopics(freshTopics);
        }

        private void UpdateTopics(IEnumerable<Topic> topics)
        {
            var topicsToAdd = new List<ITopicViewModel>();
            var topicsToRemove = new List<ITopicViewModel>(_topics);

            foreach (var topic in topics)
            {
                var existingTopic = _topics.OfType<TopicViewModel>().FirstOrDefault(t => t.Id == topic.Id);

                if (existingTopic == default)
                {
                    existingTopic = _topicFactory();
                    topicsToAdd.Add(existingTopic);
                }
                else
                {
                    topicsToRemove.Remove(existingTopic);
                }

                existingTopic.Hydrate(topic);
            }

            _topics.RemoveRange(topicsToRemove);
            _topics.AddRange(topicsToAdd);
        }
    }
}
