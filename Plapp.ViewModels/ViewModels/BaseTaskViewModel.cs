using Plapp.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plapp.ViewModels
{
    public abstract class BaseTaskViewModel : BaseViewModel, ITaskViewModel
    {
        private readonly EventWaitHandle _taskWaitHandle;

        public ICommand ConfirmCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }

        protected BaseTaskViewModel(IServiceProvider serviceProvider) : base (serviceProvider)
        {
            _taskWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            ConfirmCommand = new CommandHandler(Confirm, o => ValidateResult());
            CancelCommand = new CommandHandler(Cancel);
        }

        public Task GetAwaiter()
        {
            return Task.Run(() => _taskWaitHandle.WaitOne());
        }

        private void Confirm()
        {
            OnConfirm();
            _taskWaitHandle.Set();
        }

        private void Cancel()
        {
            _taskWaitHandle.Set();
        }

        protected abstract bool ValidateResult();
        protected abstract void OnConfirm();
    }
}
