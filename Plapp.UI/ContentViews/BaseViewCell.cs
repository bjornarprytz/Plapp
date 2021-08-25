using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace Plapp.UI.ContentViews
{
    public abstract class BaseViewCell<TViewModel> : ReactiveViewCell<TViewModel> 
        where TViewModel : class, IViewModel
    {
        protected BaseViewCell()
        {
            this.WhenActivated(DoBindings);
        }
        
        protected sealed override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.AppearingAsync();
        }

        protected sealed override async void OnDisappearing()
        {
            await ViewModel.DisappearingAsync();
            
            base.OnDisappearing();
        }
        
        protected abstract void DoBindings(CompositeDisposable bindingsDisposable);
    }
}