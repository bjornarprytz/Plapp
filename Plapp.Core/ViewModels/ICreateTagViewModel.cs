using System.Windows.Input;
using ReactiveUI;

namespace Plapp.Core
{
    public interface ICreateViewModel<out TViewModel> : IViewModel
        where TViewModel : IViewModel
    {
        ICommand ConfirmCommand { get; }
        ICommand CancelCommand { get; }
        string Error { get; }
        TViewModel ToCreate { get; }
    }
}