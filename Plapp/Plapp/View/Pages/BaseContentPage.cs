using Microsoft.Extensions.Logging;
using Plapp.Core;
using System;
using Xamarin.Forms;

namespace Plapp
{
    public abstract class BaseContentPage<TViewModel> : ContentPage
        where TViewModel : IViewModel
    {
        public TViewModel VM { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

        protected BaseContentPage(TViewModel vm) 
        {
            VM = vm;
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                VM?.OnShow();
            }
            catch (Exception ex) { IoC.Get<ILogger>().LogTrace(ex.Message); }
        }

        protected override void OnDisappearing()
        {
            try
            {
                VM?.OnHide();
                base.OnDisappearing();
            }
            catch (Exception ex) { IoC.Get<ILogger>().LogTrace(ex.Message); }
        }
    }
}
