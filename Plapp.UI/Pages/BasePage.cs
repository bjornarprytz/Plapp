using System.Reactive.Disposables;
using Plapp.Core;
using ReactiveUI.XamForms;

namespace Plapp.UI.Pages
{
    public abstract class BaseContentPage<TViewModel> : ReactiveContentPage<TViewModel> 
        where TViewModel : class, IViewModel
    {
        private readonly CompositeDisposable _bindingsDisposable = new();
        
        protected sealed override async void OnAppearing()
        {
            base.OnAppearing();
            
            DoBindings(_bindingsDisposable);

            await ViewModel.AppearingAsync();
        }

        protected sealed override async void OnDisappearing()
        {
            await ViewModel.DisappearingAsync();
            
            base.OnDisappearing();
            
            _bindingsDisposable.Clear();
        }


        protected abstract void DoBindings(CompositeDisposable bindingsDisposable);
    }
}