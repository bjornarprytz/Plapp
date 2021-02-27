using Plapp.Core;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Plapp.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        protected object mPropertyValueCheckLock = new object();

        protected IServiceProvider ServiceProvider { get; private set; }
        protected INavigator Navigator => ServiceProvider.Get<INavigator>();
        protected IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();

        protected BaseViewModel(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public bool IsShowing { get; private set; }

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

        protected async Task FlagActionAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
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

        protected async Task<T> FlagActionAsync<T>(Expression<Func<bool>> updatingFlag, Func<Task<T>> action, T defaultValue = default)
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
