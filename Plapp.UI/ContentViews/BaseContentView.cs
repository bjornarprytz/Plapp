using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;

namespace Plapp.UI.ContentViews
{
    public abstract class BaseContentView<TViewModel> : ReactiveContentView<TViewModel> 
        where TViewModel : class, IViewModel
    {
        protected BaseContentView()
        {
            ViewModel = Locator.Current.GetService<TViewModel>();
            
            this.WhenActivated(DoBindings);
        }
        
        protected abstract void DoBindings(CompositeDisposable bindingsDisposable);
    }
}