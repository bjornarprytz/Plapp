using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plapp.Core;
using Plapp.Views.Extensions;
using Plapp.Views.Popups;
using Xamarin.Forms;

namespace Plapp.Views.Infrastructure
{
    public class Prompter : IPrompter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPopupNavigation _popupNavigation;
        private readonly IViewFactory _viewFactory;
        private readonly INavigation _navigation;

        public Prompter(IServiceProvider serviceProvider, IPopupNavigation popupNavigation, IViewFactory viewFactory, INavigation navigation)
        {
            _serviceProvider = serviceProvider;
            _popupNavigation = popupNavigation;
            _viewFactory = viewFactory;
            _navigation = navigation;
        }

        public async Task<TViewModel> CreateAsync<TViewModel>(Action<TViewModel> setTemplateAction = null)
            where TViewModel : IViewModel
        {
            var popup = _viewFactory.CreatePopup<ICreateViewModel<TViewModel>>();

            setTemplateAction?.Invoke(popup.VM.Partial);

            await PopupTaskAsync(popup);

            return popup.VM.GetResult();
        }

        public async Task<IEnumerable<TViewModel>> CreateMultipleAsync<TViewModel>(Func<TViewModel> getTemplateFunc = null)
            where TViewModel : IViewModel
        {
            getTemplateFunc ??= () => _serviceProvider.Get<TViewModel>();

            var popup = _viewFactory.CreatePopup<ICreateMultipleViewModel<TViewModel>>(
                vm => 
                {
                    vm.Current = getTemplateFunc();
                    vm.TemplateFactory = getTemplateFunc;
                });


            await PopupTaskAsync(popup);

            return popup.VM.GetResult();
        }

        public async Task PopupAsync<TViewModel>() 
            where TViewModel : ITaskViewModel, IIOViewModel
        {
            var popup = _viewFactory.CreatePopup<TViewModel>();

            await PopupTaskAsync(popup);
        }

        public async Task AlertAsync(string title, string alert, string confirm)
        {
            await _navigation.CurrentPage().DisplayAlert(title, alert, confirm);
        }

        public async Task<bool> DilemmaAsync(string title, string question, string yes, string no)
        {
            return await _navigation.CurrentPage().DisplayAlert(title, question, yes, no);
        }

        public async Task<string> ChooseAsync(string title, string cancel, string destruction, params string[] options)
        {
            return await _navigation.CurrentPage().DisplayActionSheet(title, cancel, destruction, options);
        }

        public async Task<string> AnswerAsync(string title, string question)
        {
            return await _navigation.CurrentPage().DisplayPromptAsync(title, question);
        }

        public async Task<string> AnswerNumericAsync(string title, string question)
        {
            return await _navigation.CurrentPage().DisplayPromptAsync(title, question, keyboard: Keyboard.Numeric);
        }

        public async Task PopAsync()
        {
            await _popupNavigation.PopAsync();
        }

        private async Task PopupTaskAsync<TViewModel>(BasePopupPage<TViewModel> popupPage)
            where TViewModel : ITaskViewModel, IIOViewModel
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            
            _popupNavigation.Popped += PopupNavigation_Popped;

            await _popupNavigation.PushAsync(popupPage);

            await Task.Run(() => waitHandle.WaitOne());

            void PopupNavigation_Popped(object sender, PopupNavigationEventArgs e)
            {
                if (e.Page != popupPage) 
                {
                    return;
                }

                waitHandle.Set();
                _popupNavigation.Popped -= PopupNavigation_Popped;
            }
        }
    }
}
