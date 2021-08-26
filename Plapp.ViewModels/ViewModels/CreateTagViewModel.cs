using System;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;

namespace Plapp.ViewModels
{
    public class CreateTagViewModel : BaseCreateViewModel<ITagViewModel>
    {
        private readonly SourceCache<ITagViewModel, int> _tagsMutable = new (tag => tag.Id);
        private readonly ReadOnlyObservableCollection<ITagViewModel> _availableTags;
        private readonly IMediator _mediator;

        public CreateTagViewModel(
            IViewModelFactory vmFactory,
            IPrompter prompter,
            IMediator mediator
            ) : base(() => vmFactory.Create<ITagViewModel>(), prompter)
        {
            _mediator = mediator;

            _tagsMutable
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _availableTags)
                .Subscribe();
        }


        protected override bool PartialIsValid()
        {
            return Partial != null;
        }

        public override Task AppearingAsync()
        {
            return LoadTagsAsync();
        }
 
        private async Task LoadTagsAsync()
        {
            var tagsResponse = await _mediator.Send(new GetAllTagsQuery());

            if (tagsResponse.IsError)
                tagsResponse.Throw();

            var tags = tagsResponse.Payload;

            _tagsMutable.Edit(innerList =>
            {
                innerList.Clear();
                innerList.AddOrUpdate(tags);
            });
        }
    }
}
