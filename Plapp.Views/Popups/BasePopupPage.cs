using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using ReactiveUI;
using Rg.Plugins.Popup.Pages;

namespace Plapp.Views.Popups
{
    public abstract class BasePopupPage<TViewModel> : PopupPage, IViewFor<TViewModel>
        where TViewModel : class, ITaskViewModel
    {
        private readonly ILogger _logger;
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TViewModel)value;
        }
        
        public TViewModel ViewModel { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

        protected BasePopupPage(TViewModel viewModel, ILogger logger)
        {
            ViewModel = viewModel;

            _logger = logger;

            BackgroundClicked += BasePopupPage_BackgroundClicked;        }

        private void BasePopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            ViewModel.CancelCommand.Execute(null);
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await ViewModel.AppearingAsync();
            }
            catch (Exception ex) { _logger.LogTrace(ex.Message); }
        }

        protected override async void OnDisappearing()
        {
            try
            {
                await ViewModel.DisappearingAsync();
                base.OnDisappearing();
            }
            catch (Exception ex) { _logger.LogTrace(ex.Message); }
        }

        
    }
}
