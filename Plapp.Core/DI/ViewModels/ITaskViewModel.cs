using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITaskViewModel : IViewModel
    {
        bool IsConfirmed { get; }

        ICommand ConfirmCommand { get; }
        ICommand CancelCommand { get; }
    }
}
