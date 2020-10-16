using Plapp.Core;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class CameraViewModel : BaseViewModel, ICameraViewModel
    {
        public CameraViewModel()
        {
            TakePhotoCommand = new CommandHandler(async () => await TakePhoto());
            CancelCommand = new CommandHandler(async () => await Cancel());
        }

        public ICommand TakePhotoCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private async Task TakePhoto()
        {
            var result = await IoC.Get<ICamera>().TakePhotoAsync();
        }

        private async Task Cancel()
        {
            await NavigationHelpers.Back();
        }
    }
}
