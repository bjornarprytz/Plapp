using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plapp.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IViewModel
    {
        protected CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public virtual Task AppearingAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task DisappearingAsync()
        {
            return Task.CompletedTask;
        }
    }
}
