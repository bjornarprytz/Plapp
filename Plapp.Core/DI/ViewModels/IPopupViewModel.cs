using System.Windows.Input;

namespace Plapp.Core
{
    public interface IPopupViewModel : IViewModel
    {
        bool IsCancelled { get; }
        string Text { get; set; }
        ICommand ConfirmCommand { get; }
        ICommand CancelCommand { get; }
    }
}
