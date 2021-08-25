using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;
using DynamicData;
using Plapp.BusinessLogic.Interactive;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        private readonly SourceList<ITopicViewModel> _topics = new();

        private readonly IViewModelFactory _vmFactory;
        private readonly IMediator _mediator;

        public ApplicationViewModel(
            IViewModelFactory vmFactory,
            IMediator mediator
            )
        {
            _vmFactory = vmFactory;
            _mediator = mediator;

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new AsyncCommand<ITopicViewModel>(DeleteTopic, allowsMultipleExecutions: false);
        }

        public override Task AppearingAsync()
        {
            return LoadTopicsAsync();
        }


        //public extern IEnumerable<ITopicViewModel> FlashyTopics { [ObservableAsProperty] get; private set; }
        //[Reactive] private string FlashyThing { get; set; }

        public IObservableList<ITopicViewModel> Topics => _topics.AsObservableList();
        
        public IAsyncCommand AddTopicCommand { get; private set; }
        public IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; private set; }

        private async Task LoadTopicsAsync(CancellationToken cancellationToken=default)
        {
            var freshTopicResponse = await _mediator.Send(new GetAllTopicsQuery(), cancellationToken);

            if (freshTopicResponse.IsError)
                freshTopicResponse.Throw();

            var freshTopics = freshTopicResponse.Payload;

            _topics.Edit(innerList => // TODO: Avoid updating unchanged items
            {
                innerList.Clear();
                innerList.AddRange(freshTopics);
            });
        }

        private async Task AddTopic()
        {
            var newTopic = _vmFactory.Create<ITopicViewModel>();

            _topics.Add(newTopic);

            await _mediator.Send(new NavigateAction("topic")); // TODO: Specify route to new topic
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            await _mediator.Send(new DeleteTopicCommand(topic));

            _topics.Remove(topic);
        }
    }
}
