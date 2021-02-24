﻿using Plapp.Core;
using Rg.Plugins.Popup.Contracts;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plapp
{
    public class Prompter : IPrompter
    {
        private readonly IServiceProvider _serviceProvider;
        private IPopupNavigation PopupNavigation => _serviceProvider.Get<IPopupNavigation>();
        private IViewFactory ViewFactory => _serviceProvider.Get<IViewFactory>();
        private INavigation Navigation => _serviceProvider.Get<INavigation>();

        public Prompter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TViewModel> CreateAsync<TViewModel>()
            where TViewModel : IViewModel
        {
            var popup = ViewFactory.CreatePopup<ICreateViewModel<TViewModel>>();

            await PopupNavigation.PushAsync(popup);

            var vm = await popup.VM.Creation();

            await PopupNavigation.PopAsync();

            return vm;
        }

        public async Task AlertAsync(string title, string alert, string confirm)
        {
            await Navigation.CurrentPage().DisplayAlert(title, alert, confirm);
        }

        public async Task<bool> DilemmaAsync(string title, string question, string yes, string no)
        {
            return await Navigation.CurrentPage().DisplayAlert(title, question, yes, no);
        }

        public async Task<string> ChooseAsync(string title, string cancel, string destruction, params string[] options)
        {
            return await Navigation.CurrentPage().DisplayActionSheet(title, cancel, destruction, options);
        }

        public async Task<string> AnswerAsync(string title, string question)
        {
            return await Navigation.CurrentPage().DisplayPromptAsync(title, question);
        }

        public async Task<string> AnswerNumericAsync(string title, string question)
        {
            return await Navigation.CurrentPage().DisplayPromptAsync(title, question, keyboard: Keyboard.Numeric);
        }
    }
}
