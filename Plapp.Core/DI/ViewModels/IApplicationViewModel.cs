using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IViewModel
    {
        bool IsLoadingTopics { get; }
        ObservableCollection<ITopicViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
        ICommand DeleteTopicCommand { get; }
    }
}
