using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ITaskViewModel : IViewModel
    {
        Task GetAwaiter();

        ICommand ConfirmCommand { get; }
        ICommand CancelCommand { get; }
    }
}
