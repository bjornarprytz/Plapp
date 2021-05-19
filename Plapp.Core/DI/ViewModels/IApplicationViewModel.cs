using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IIOViewModel
    {
        ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
        ICommand DeleteTopicCommand { get; }
    }
}
