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
        
        public CreateTagViewModel(IServiceProvider serviceProvider)
            :base(serviceProvider)
        {
            _availableTags = new ObservableCollection<ITagViewModel>();
            AvailableTags = new ReadOnlyObservableCollection<ITagViewModel>(_availableTags);
        }

        public ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }

        protected override bool PartialIsValid()
        {
            return Partial != null;
        }

        protected override void OnConfirm()
        {
            base.OnConfirm();

            Task.Run(SaveResult);
        }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var tags = await DataStore.FetchTagsAsync();

            _availableTags.AddRange(tags.Select(tag => tag.ToViewModel(ServiceProvider)));
        }
            
        protected async Task SaveResult()
        {
            await FlagActionAsync(
                () => IsSavingData,
                async () =>
                {
                    Partial.Color = "#FFA500";
                    await DataStore.SaveTagAsync(Partial.ToModel());
                });

        }
    }
}
