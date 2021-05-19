using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IIOViewModel
    {
        ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        IAsyncCommand AddTopicCommand { get; }
        ICommand DeleteTopicCommand { get; }
    }
}
