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

        public CreateTagViewModel(
            IViewModelFactory vmFactory,
            IPrompter prompter,
            IMediator mediator
            ) : base(() => vmFactory.Create<ITagViewModel>(), prompter)
        {
            _mediator = mediator;

            AvailableTags = new ObservableCollection<ITagViewModel>();
        }

        public ObservableCollection<ITagViewModel> AvailableTags { get; }

        protected override bool PartialIsValid()
        {
            return Partial != null;
        }

        protected override async Task AutoLoadDataAsync()
        {
            var tagsResponse = await _mediator.Send(new GetAllTagsQuery());

            if (tagsResponse.Error)
                tagsResponse.Throw();

            var tags = tagsResponse.Data;

            AvailableTags.Update(
                tags,
                (v1, v2) => v1.Id == v2.Id);
        }
    }
}
