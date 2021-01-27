using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plapp
{
    public abstract class BaseContentPage<TViewModel> : ContentPage
        where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

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
