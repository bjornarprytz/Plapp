using System;
using Plapp.Core;
using Xamarin.Forms;

namespace Plapp.Views.Pages
{
    public abstract class BaseContentPage<TViewModel> : ContentPage
        where TViewModel : IViewModel
    {
        private readonly ILogger _logger;

        public TViewModel VM { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

        protected BaseContentPage(TViewModel vm, ILogger logger) 
        {
            VM = vm;
            _logger = logger;
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                VM?.OnShow();
            }
            catch (Exception ex) { _logger.LogTrace(ex.Message); }
        }

        protected override void OnDisappearing()
        {
            try
            {
                VM?.OnHide();
                base.OnDisappearing();
            }
            catch (Exception ex) { _logger.LogTrace(ex.Message); }
        }
    }
}
