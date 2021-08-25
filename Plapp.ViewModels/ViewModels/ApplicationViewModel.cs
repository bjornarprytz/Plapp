using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DynamicData;
using Plapp.BusinessLogic.Interactive;
using ReactiveUI;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        private readonly SourceCache<ITopicViewModel, int> _topicsMutable = new (topic => topic.Id);
        private readonly ReadOnlyObservableCollection<ITopicViewModel> _topics;

        private readonly IViewModelFactory _vmFactory;
        private readonly IMediator _mediator;

        public ApplicationViewModel(
            IViewModelFactory vmFactory,
            IMediator mediator)
        {
            _vmFactory = vmFactory;
            _mediator = mediator;

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new AsyncCommand<ITopicViewModel>(DeleteTopic, allowsMultipleExecutions: false);

            _topicsMutable 
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _topics)
                .DisposeMany()
                .Subscribe();

        }

        public override Task AppearingAsync()
        {
            return LoadTopicsAsync();
        }


        //public extern IEnumerable<ITopicViewModel> FlashyTopics { [ObservableAsProperty] get; private set; }
        //[Reactive] private string FlashyThing { get; set; }

        public ReadOnlyObservableCollection<ITopicViewModel> Topics => _topics;

        public IAsyncCommand AddTopicCommand { get; private set; }
        public IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; private set; }

        private async Task LoadTopicsAsync(CancellationToken cancellationToken=default)
        {
            var freshTopicResponse = await _mediator.Send(new GetAllTopicsQuery(), cancellationToken);

            if (freshTopicResponse.IsError)
                freshTopicResponse.Throw();

            var freshTopics = freshTopicResponse.Payload;

            _topicsMutable.Edit(innerList =>
            {
                innerList.Clear();
                innerList.AddOrUpdate(freshTopics);
            });
        }

        private async Task AddTopic()
        {
            var newTopic = _vmFactory.Create<ITopicViewModel>();

            _topicsMutable.AddOrUpdate(newTopic);

            await _mediator.Send(new NavigateAction("topic")); // TODO: Specify route to new topic
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            await _mediator.Send(new DeleteTopicCommand(topic));

            _topicsMutable.Remove(topic);
        }
    }
}
