using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;

namespace Plapp.UI.Pages
{
    public abstract class BaseContentPage<TViewModel> : ReactiveContentPage<TViewModel> 
        where TViewModel : class, IViewModel
    {
        protected BaseContentPage()
        {
            ViewModel = Locator.Current.GetService<TViewModel>();
            
            this.WhenActivated(DoBindings);
        }
        
        protected sealed override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel != null) await ViewModel.AppearingAsync();
        }

        protected sealed override async void OnDisappearing()
        {
            if (ViewModel != null) await ViewModel.DisappearingAsync();

            base.OnDisappearing();
        }


        protected abstract void DoBindings(CompositeDisposable bindingsDisposable);
    }
}