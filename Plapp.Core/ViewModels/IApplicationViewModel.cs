using System.Collections.ObjectModel;
using System.Windows.Input;
using DynamicData;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IViewModel
    {
        IObservableList<ITopicViewModel> Topics { get; }
        IAsyncCommand AddTopicCommand { get; }
        IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; }
    }
}
