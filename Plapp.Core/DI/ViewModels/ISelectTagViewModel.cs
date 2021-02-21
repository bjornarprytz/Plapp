
using System.Collections.ObjectModel;

namespace Plapp.Core
{
    public interface ISelectTagViewModel : IViewModel
    {
        bool IsLoadingTags { get; }
        ReadOnlyObservableCollection<ITagViewModel> AvailableTags { get; }
    }
}
