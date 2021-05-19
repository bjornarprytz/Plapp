﻿using Plapp.Core;
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
        private readonly ITagService _tagService;

        public CreateTagViewModel(
            ViewModelFactory<ITagViewModel> tagFactory,
            IPrompter prompter,
            ITagService tagService
            ) : base(tagFactory, prompter)
        {
            _tagFactory = tagFactory;
            _tagService = tagService;

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
            var tags = await _tagService.FetchAllAsync();

            _availableTags.AddRange(tags.Select(tag => (ITagViewModel) tag.ToViewModel(() => _tagFactory() as TagViewModel)));
        }
    }
}
