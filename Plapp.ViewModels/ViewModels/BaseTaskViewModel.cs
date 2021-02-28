using Plapp.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public abstract class BaseTaskViewModel : BaseViewModel, ITaskViewModel
    {
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
            await Prompter.PopAsync();
        }

        private async Task Cancel()
        {
            await Prompter.PopAsync();
        }

        protected abstract bool CanConfirm();
        protected virtual void OnConfirm()
        {
            IsConfirmed = true;
        }
    }
}
