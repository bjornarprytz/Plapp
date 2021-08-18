using System;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.Views.Pages
{
    public abstract class BaseContentPage<TViewModel> : ReactiveContentPage<TViewModel>
        where TViewModel : class, IViewModel 
    {
        public TViewModel VM 
        { 
            get => (TViewModel)BindingContext;
            set => BindingContext = value;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await VM.AppearingAsync();
        }

        protected override async void OnDisappearing()
        {
            await VM.DisappearingAsync();
            
            base.OnDisappearing();
        }
    }
}
