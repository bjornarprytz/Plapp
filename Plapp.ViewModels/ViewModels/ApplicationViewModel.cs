using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : PageViewModel, IApplicationViewModel
    {
        private readonly ObservableCollection<ITopicViewModel> _topics;

        public ApplicationViewModel(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _topics = new ObservableCollection<ITopicViewModel>();
            Topics = new ReadOnlyObservableCollection<ITopicViewModel>(_topics);

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new CommandHandler<ITopicViewModel>(DeleteTopic);
        }

        public ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        
        public ICommand AddTopicCommand { get; private set; }
        public ICommand DeleteTopicCommand { get; private set; }

        private async Task AddTopic()
        {
            var newTopic = ServiceProvider.Get<ITopicViewModel>();

            _topics.Add(newTopic);

            _ = TopicService.SaveAsync(newTopic.ToModel());

            await Navigator.GoToAsync(newTopic);
        }

        private void DeleteTopic(ITopicViewModel topic)
        {
            _topics.Remove(topic);
            
            Task.Run(() => TopicService.DeleteAsync(topic.ToModel()));
        }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var freshTopics = await TopicService.FetchAllAsync();

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
                    topicsToAdd.Add(topic.ToViewModel(ServiceProvider));
                }
                else
                {
                    existingTopic.Hydrate(topic);
                    topicsToRemove.Remove(existingTopic);
                }
            }

            _topics.RemoveRange(topicsToRemove);
            _topics.AddRange(topicsToAdd);
        }
    }
}
