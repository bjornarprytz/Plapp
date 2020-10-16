using System.Windows.Input;

namespace Plapp.Core
{
    public interface ICameraViewModel : IViewModel
    {
        ICommand TakePhotoCommand { get; }
        ICommand CancelCommand { get; }
    }
}
