using System;
using System.Collections;
using System.Collections.Generic;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Commands;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _vmFactory;
        private readonly IMediator _mediator;

        public ApplicationViewModel(
            INavigator navigator,
            IViewModelFactory vmFactory,
            IMediator mediator
            )
        {
            _navigator = navigator;
            _vmFactory = vmFactory;
            _mediator = mediator;

            Topics = new ObservableCollection<ITopicViewModel>();

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new AsyncCommand<ITopicViewModel>(DeleteTopic, allowsMultipleExecutions: false);
/*
 * 
            FlashyTopics = this.WhenAnyValue(x => FlashyThing)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Where(term => !string.IsNullOrWhiteSpace(term))
                
                //.SelectMany(GetTOpics)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.FlashyTopics);
 */

            // TODO: Load topics on IsShowing==True
        }

        
        //public extern IEnumerable<ITopicViewModel> FlashyTopics { [ObservableAsProperty] get; private set; }
        //[Reactive] private string FlashyThing { get; set; }
        
        public ObservableCollection<ITopicViewModel> Topics { get; }
        
        public IAsyncCommand AddTopicCommand { get; private set; }
        public IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; private set; }

        /*
         * 
        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var freshTopicResponse = await _mediator.Send(new GetAllTopicsQuery());

            if (freshTopicResponse.IsError)
                freshTopicResponse.Throw();

            var freshTopics = freshTopicResponse.Payload;

            Topics.Update(
                freshTopics,
                (v1, v2) => v1.Id == v2.Id);
        }
         */

        private async Task AddTopic()
        {
            var newTopic = _vmFactory.Create<ITopicViewModel>();

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
