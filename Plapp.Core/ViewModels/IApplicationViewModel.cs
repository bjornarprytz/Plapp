using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface IApplicationViewModel : IViewModel
    {
        ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        ICommand AddTopicCommand { get; }
        ICommand AddTagCommand { get; }
    }
}
