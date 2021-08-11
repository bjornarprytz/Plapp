using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plapp.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IViewModel
    {
        protected extern bool IsLoading { [ObservableAsProperty] get; }
        [Reactive] public bool IsShowing { get; set; }

        public virtual Task AppearingAsync()
        {
            IsShowing = true;
            return Task.CompletedTask;
        }

        public virtual Task DisappearingAsync()
        {
            IsShowing = false;
            return Task.CompletedTask;
        }
    }
}
