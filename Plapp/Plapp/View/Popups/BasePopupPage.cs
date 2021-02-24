﻿using Microsoft.Extensions.Logging;
using Plapp.Core;
using Rg.Plugins.Popup.Pages;
using System;

namespace Plapp
{
    public abstract class BasePopupPage<TViewModel> : PopupPage
        where TViewModel : IViewModel
    {
        public TViewModel VM { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

        protected BasePopupPage()
        {
            VM = IoC.Get<TViewModel>();
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
