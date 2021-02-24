using Plapp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    class CreateTagViewModel : BaseCreateViewModel<ITagViewModel>
    {
        private readonly ObservableCollection<ITagViewModel> _availableTags;

        private IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();

        public CreateTagViewModel(IServiceProvider serviceProvider)
            :base(serviceProvider)
        {
            _availableTags = new ObservableCollection<ITagViewModel>();
            AvailableTags = new ReadOnlyObservableCollection<ITagViewModel>(_availableTags);
        }

        public bool IsLoadingTags { get; private set; }

        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

        public override IViewModel UnderCreation => throw new NotImplementedException();

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
        public override Task<ITagViewModel> Creation()
        {
            throw new NotImplementedException();
        }
    }
}
