using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Plapp
{
    public interface IDiaryViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; }
        ObservableCollection<ITopicViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
    }
}
