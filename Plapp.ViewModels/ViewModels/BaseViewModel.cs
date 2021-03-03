﻿using Plapp.Core;
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
        protected IPrompter Prompter => ServiceProvider.Get<IPrompter>();
        protected IPlappDataStore DataStore => ServiceProvider.Get<IPlappDataStore>();

        protected BaseViewModel(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

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
