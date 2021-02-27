using Plapp.Core;
using System;
using System.Threading;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public abstract class BaseTaskViewModel : BaseViewModel, ITaskViewModel
    {
        public EventWaitHandle TaskWaitHandle { get; }
        public ICommand ConfirmCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }

        protected BaseTaskViewModel(IServiceProvider serviceProvider) : base (serviceProvider)
        {
            TaskWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            ConfirmCommand = new CommandHandler(Confirm, o => ValidateResult());
            CancelCommand = new CommandHandler(Cancel);
        }

        private void Confirm()
        {
            TaskWaitHandle.Set();
        }

        private void Cancel()
        {
            TaskWaitHandle.Set();
        }

        protected abstract bool ValidateResult();
    }
}
