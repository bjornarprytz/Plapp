using System;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using Rg.Plugins.Popup.Pages;

namespace Plapp.Views.Popups
{
    public abstract class BasePopupPage<TViewModel> : PopupPage
        where TViewModel : ITaskViewModel
    {
        private readonly ILogger _logger;

        public TViewModel VM { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

        protected BasePopupPage(TViewModel vm, ILogger logger)
        {
            VM = vm;

            _logger = logger;

            BackgroundClicked += BasePopupPage_BackgroundClicked;        }

        private void BasePopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            VM.CancelCommand.Execute(null);
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
