using AutoMapper;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : IOViewModel, IApplicationViewModel
    {
        private readonly INavigator _navigator;
        private readonly IMediator _mediator;

        public ApplicationViewModel(
            INavigator navigator,
            IMediator mediator
            )
        {
            _navigator = navigator;
            _mediator = mediator;

            Topics = new ObservableCollection<ITopicViewModel>();

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new AsyncCommand<ITopicViewModel>(DeleteTopic, allowsMultipleExecutions: false);
        }

        public ObservableCollection<ITopicViewModel> Topics { get; }
        
        public IAsyncCommand AddTopicCommand { get; private set; }
        public IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; private set; }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var freshTopicResponse = await _mediator.Send(new GetAllTopicsQuery());

            if (freshTopicResponse.Error)
                freshTopicResponse.Throw();

            var freshTopics = freshTopicResponse.Data;

            Topics.Update(
                freshTopics,
                (v1, v2) => v1.Id == v2.Id);
        }

        private async Task AddTopic()
        {
            var topicResponse = await _mediator.Send(new CreateTopicCommand());

            if (topicResponse.Error)
                topicResponse.Throw();

            var newTopic = topicResponse.Data;

            Topics.Add(newTopic);

            await _navigator.GoToAsync(newTopic);
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            await _mediator.Send(new DeleteTopicCommand(topic));

            Topics.Remove(topic);
        }
    }
}
