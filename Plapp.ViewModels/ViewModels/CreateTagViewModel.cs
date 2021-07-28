using AutoMapper;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public class CreateTagViewModel : BaseCreateViewModel<ITagViewModel>
    {
        private readonly ObservableCollection<ITagViewModel> _availableTags;
        private readonly ViewModelFactory<ITagViewModel> _tagFactory;
        private readonly IPrompter _prompter;
        private readonly IMediator _mediator;

        public CreateTagViewModel(
            ViewModelFactory<ITagViewModel> tagFactory,
            IPrompter prompter,
            IMediator mediator
            ) : base(tagFactory, prompter)
        {
            _tagFactory = tagFactory;
            _prompter = prompter;
            _mediator = mediator;

            _availableTags = new ObservableCollection<ITagViewModel>();
            AvailableTags = new ReadOnlyObservableCollection<ITagViewModel>(_availableTags);
        }

        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

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

            _availableTags.Update(
                tags,
                (v1, v2) => v1.Id == v2.Id);
        }
    }
}
