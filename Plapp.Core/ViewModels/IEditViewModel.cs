using System.Windows.Input;

namespace Plapp.Core
{
    public interface IEditViewModel<out TViewModel> : IViewModel
        where TViewModel : IViewModel
    {
        ICommand ConfirmCommand { get; }
        ICommand CancelCommand { get; }
        string Error { get; }
        TViewModel ToCreate { get; }
    }
}