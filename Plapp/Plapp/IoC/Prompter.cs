using Plapp.Core;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Events;
using System;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<TViewModel> CreateAsync<TViewModel>(Action<TViewModel> setTemplateAction = null)
            where TViewModel : IViewModel
        {
            var popup = ViewFactory.CreatePopup<ICreateViewModel<TViewModel>>();

            setTemplateAction?.Invoke(popup.VM.Partial);

            await PopupTaskAsync(popup);

            return popup.VM.GetResult();
        }

        public async Task<IEnumerable<TViewModel>> CreateMultipleAsync<TViewModel>(Func<TViewModel> getTemplateFunc = null)
            where TViewModel : IViewModel
        {
            getTemplateFunc ??= () => _serviceProvider.Get<TViewModel>();

            var popup = ViewFactory.CreatePopup<ICreateMultipleViewModel<TViewModel>>(
                vm => 
                {
                    vm.Current = getTemplateFunc();
                    vm.TemplateFunc = getTemplateFunc;
                });


            await PopupTaskAsync(popup);

            return popup.VM.GetResult();
        }

        public async Task PopupAsync<TViewModel>() 
            where TViewModel : ITaskViewModel, IRootViewModel
        {
            var popup = ViewFactory.CreatePopup<TViewModel>();

            await PopupTaskAsync(popup);
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

        public async Task PopAsync()
        {
            await PopupNavigation.PopAsync();
        }

        private async Task PopupTaskAsync<TViewModel>(BasePopupPage<TViewModel> popupPage)
            where TViewModel : ITaskViewModel, IRootViewModel
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            
            PopupNavigation.Popped += PopupNavigation_Popped;

            await PopupNavigation.PushAsync(popupPage);

            await Task.Run(() => waitHandle.WaitOne());

            void PopupNavigation_Popped(object sender, PopupNavigationEventArgs e)
            {
                if (e.Page != popupPage) 
                {
                    return;
                }

                waitHandle.Set();
                PopupNavigation.Popped -= PopupNavigation_Popped;
            }
        }
    }
}
