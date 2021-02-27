﻿using Plapp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class CreateTagViewModel : BaseCreateViewModel<ITagViewModel>
    {
        private readonly ObservableCollection<ITagViewModel> _availableTags;
        
        public CreateTagViewModel(IServiceProvider serviceProvider)
            :base(serviceProvider)
        {
            _availableTags = new ObservableCollection<ITagViewModel>();
            AvailableTags = new ReadOnlyObservableCollection<ITagViewModel>(_availableTags);

            Result = ServiceProvider.Get<ITagViewModel>();
        }

        public bool IsLoadingTags { get; private set; }

        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

        public override void OnShow()
        {
            base.OnShow();

            Task.Run(LoadTags);
        }

        protected override bool ValidateResult()
        {
            return Result != null;
        }

        private async Task LoadTags()
        {
            await FlagActionAsync(
                () => IsLoadingTags,
                async () =>
                {
                    var tags = await DataStore.FetchTagsAsync();

                    _availableTags.AddRange(tags.Select(tag => tag.ToViewModel(ServiceProvider)));
                });
        }
    }
}
