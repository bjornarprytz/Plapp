using Plapp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    class SelectTagViewModel : BaseViewModel, ISelectTagViewModel
    {
        private readonly ObservableCollection<ITagViewModel> _availableTags;

        private IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();

        public SelectTagViewModel(IServiceProvider serviceProvider)
            :base(serviceProvider)
        {
            _availableTags = new ObservableCollection<ITagViewModel>();
            AvailableTags = new ReadOnlyObservableCollection<ITagViewModel>(_availableTags);
        }

        public bool IsLoadingTags { get; private set; }

        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

        public override void OnShow()
        {
            base.OnShow();

            Task.Run(LoadTags);
        }

        private async Task LoadTags()
        {
            await RunCommandAsync(
                () => IsLoadingTags,
                async () =>
                {
                    var tags = await DataStore.FetchTagsAsync();

                    _availableTags.AddRange(tags.Select(tag => tag.ToViewModel(ServiceProvider)));
                });
        }
    }
}
