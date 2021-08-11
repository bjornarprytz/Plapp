using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IViewModel
    {
        ObservableCollection<ITopicViewModel> Topics { get; }
        IAsyncCommand AddTopicCommand { get; }
        IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; }
    }
}
