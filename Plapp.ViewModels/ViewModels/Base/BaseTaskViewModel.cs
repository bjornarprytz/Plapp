using Plapp.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public abstract class BaseTaskViewModel : BaseViewModel, ITaskViewModel
    {
        private readonly IPrompter _prompter;

        public bool IsConfirmed { get; protected set; }

        protected BaseTaskViewModel(IPrompter prompter)
        {
            ConfirmCommand = new AsyncCommand(Confirm, o => CanConfirm(), allowsMultipleExecutions: false);
            CancelCommand = new AsyncCommand(Cancel, allowsMultipleExecutions: false);

            PropertyChanged += (s, e) => (ConfirmCommand as AsyncCommand).RaiseCanExecuteChanged(); // NOTE: Can this cause a memory leak?
            _prompter = prompter;
        }

        public ICommand ConfirmCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }

        private async Task Confirm()
        {
            OnConfirm();
            await _prompter.PopAsync();
        }

        private async Task Cancel()
        {
            await _prompter.PopAsync();
        }

        protected abstract bool CanConfirm();
        protected virtual void OnConfirm()
        {
            IsConfirmed = true;
        }
    }
}
