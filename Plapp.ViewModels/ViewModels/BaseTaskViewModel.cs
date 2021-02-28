using Plapp.Core;
using Rg.Plugins.Popup.Contracts;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public abstract class BaseTaskViewModel : BaseViewModel, ITaskViewModel
    {
        protected IPopupNavigation PopupNavigation => ServiceProvider.Get<IPopupNavigation>();
        public bool IsConfirmed { get; protected set; }

        protected BaseTaskViewModel(IServiceProvider serviceProvider) : base (serviceProvider)
        {
            ConfirmCommand = new AsyncCommand(Confirm, o => CanConfirm(), allowsMultipleExecutions: false);
            CancelCommand = new AsyncCommand(Cancel, allowsMultipleExecutions: false);
        }
        public ICommand ConfirmCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }

        private async Task Confirm()
        {
            OnConfirm();
            await PopupNavigation.PopAsync();
        }

        private async Task Cancel()
        {
            await PopupNavigation.PopAsync();
        }

        protected abstract bool CanConfirm();
        protected virtual void OnConfirm()
        {
            IsConfirmed = true;
        }
    }
}
