using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;
using Xamarin.Forms;

namespace Plapp
{
    public abstract class BaseContentPage<TViewModel> : ContentPage
        where TViewModel : IViewModel
    {
        public TViewModel ViewModel { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

        protected BaseContentPage()
        {
            ViewModel = IoC.Get<TViewModel>();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                ViewModel?.OnShow();
            }
            catch (Exception ex) { IoC.Get<ILogger>().LogTrace(ex.Message); }
        }

        protected override void OnDisappearing()
        {
            try
            {
                ViewModel?.OnHide();
                base.OnDisappearing();
            }
            catch (Exception ex) { IoC.Get<ILogger>().LogTrace(ex.Message); }
        }
    }
}
