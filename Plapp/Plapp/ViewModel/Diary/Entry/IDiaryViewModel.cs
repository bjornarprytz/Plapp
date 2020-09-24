using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp
{
    public interface IDiaryViewModel : IViewModel
    {
        bool IsBusy { get; }
        ObservableCollection<ITopicViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
    }
}
