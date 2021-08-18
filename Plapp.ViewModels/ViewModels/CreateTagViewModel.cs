using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public class CreateTagViewModel : BaseCreateViewModel<ITagViewModel>
    {
        private readonly IMediator _mediator;
        private readonly ObservableCollection<ITagViewModel> _availableTags;

        public CreateTagViewModel(
            IViewModelFactory vmFactory,
            IPrompter prompter,
            IMediator mediator
            ) : base(() => vmFactory.Create<ITagViewModel>(), prompter)
        {
            _mediator = mediator;

            _availableTags = new ObservableCollection<ITagViewModel>();
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

            _availableTags.Update(
                tags,
                (v1, v2) => v1.Id == v2.Id);
        }
    }
}
