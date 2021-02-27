using System.Threading;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITaskViewModel : IViewModel
    {
        EventWaitHandle TaskWaitHandle { get; }
        ICommand ConfirmCommand { get; }
        ICommand CancelCommand { get; }
    }
}
