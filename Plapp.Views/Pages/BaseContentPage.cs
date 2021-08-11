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
        private readonly ILogger _logger;

        public TViewModel VM 
        { 
            get => (TViewModel)BindingContext;
            set => BindingContext = value;
        }

        protected BaseContentPage(TViewModel vm, ILogger logger) 
        {
            VM = vm;
            _logger = logger;
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                await VM.AppearingAsync();
            }
            catch (Exception ex) { _logger.LogTrace(ex.Message); }
        }

        protected override async void OnDisappearing()
        {
            try
            {
                await VM.DisappearingAsync();
                
                base.OnDisappearing();

            }
            catch (Exception ex) { _logger.LogTrace(ex.Message); }
        }
    }
}
