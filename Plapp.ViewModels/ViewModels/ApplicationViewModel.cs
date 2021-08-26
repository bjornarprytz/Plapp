using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Threading;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        private readonly SourceCache<ITopicViewModel, int> _topicsMutable = new (topic => topic.Id);
        private readonly ReadOnlyObservableCollection<ITopicViewModel> _topics;

        private readonly IMediator _mediator;

        public ApplicationViewModel(IMediator mediator)
        {
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
            await Shell.Current.GoToAsync($"topic");
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            await _mediator.Send(new DeleteTopicCommand(topic));

            _topicsMutable.Remove(topic);
        }
    }
}
