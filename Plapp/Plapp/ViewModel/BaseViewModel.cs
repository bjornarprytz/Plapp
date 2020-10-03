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

        public void SetState<T>(Action<T> action)
            where T : class, IViewModel
        {
            action(this as T);
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
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
