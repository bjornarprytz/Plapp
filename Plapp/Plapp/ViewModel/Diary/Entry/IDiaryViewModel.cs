using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp
{
    public interface IDiaryViewModel : IViewModel
    {
        bool IsBusy { get; }
        ObservableCollection<ITopicMetaDataViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
    }
}
