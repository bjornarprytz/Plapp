using Plapp.Core;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Plapp
{
    public abstract class BaseViewModel : IViewModel
    {
        protected object mPropertyValueCheckLock = new object();

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public bool IsShowing { get; private set; }

        public void SetState<TViewModel>(Action<TViewModel> action)
            where TViewModel : class, IViewModel
        {
            action(this as TViewModel);
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public virtual void OnShow()
        {
            IsShowing = true;
        }

        public virtual void OnHide()
        {
            IsShowing = false;
            OnUserInteractionStopped();
        }

        public virtual void OnUserInteractionStopped() { }

        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            lock (mPropertyValueCheckLock)
            {
                if (updatingFlag.GetPropertyValue())
                    return;

                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }

        protected async Task<T> RunCommandAsync<T>(Expression<Func<bool>> updatingFlag, Func<Task<T>> action, T defaultValue = default)
        {
            lock (mPropertyValueCheckLock)
            {
                if (updatingFlag.GetPropertyValue())
                    return defaultValue;

                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                return await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}
