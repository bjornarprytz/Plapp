using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IRootViewModel
    {
        bool IsLoadingTopics { get; }
        ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
        ICommand DeleteTopicCommand { get; }
    }
}
