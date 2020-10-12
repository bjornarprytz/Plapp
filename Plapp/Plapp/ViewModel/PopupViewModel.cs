using Plapp.Core;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp
{
    public class PopupViewModel : BaseViewModel, IPopupViewModel
    {
        public PopupViewModel()
        {
            CancelCommand = new CommandHandler(async () => await Cancel());
            ConfirmCommand = new CommandHandler(async () => await Confirm());
        }
        
        public bool IsCancelled { get; private set; }
        public string Text { get; set; }

        public ICommand ConfirmCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private async Task Confirm()
        {
        }

        private async Task Cancel()
        {
            IsCancelled = true;
        }
    }
}
