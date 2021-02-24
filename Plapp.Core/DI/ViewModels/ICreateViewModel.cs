using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.Core
{
    public interface ICreateViewModel<TViewModel> : IViewModel
        where TViewModel : IViewModel
    {
        IViewModel UnderCreation { get; set; }
        Task<TViewModel> Creation();

        ICommand ConfirmCommand { get; }
        ICommand CancelCommand { get; }
    }
}
