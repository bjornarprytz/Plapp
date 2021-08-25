using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IViewModel
    {
        ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        IAsyncCommand AddTopicCommand { get; }
        IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; }
    }
}
