using Plapp.Core;
using System;
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

            _ = DataStore.SaveTopicAsync(newTopic.ToModel());

            await Navigator.GoToAsync(newTopic);
        }

        private void DeleteTopic(ITopicViewModel topic)
        {
            _topics.Remove(topic);
            
            Task.Run(() => DataStore.DeleteTopicAsync(topic.ToModel()));
        }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var topics = await DataStore.FetchTopicsAsync();

            _topics.Clear();
            _topics.AddRange(topics.Select(t => t.ToViewModel(ServiceProvider)));
        }
    }
}
