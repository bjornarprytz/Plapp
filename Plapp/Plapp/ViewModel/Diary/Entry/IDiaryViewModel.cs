using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Plapp
{
    public interface IDiaryViewModel : IViewModel
    {
        int Something { get; }
        bool IsBusy { get; }
        ObservableCollection<ITopicViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
    }
}
