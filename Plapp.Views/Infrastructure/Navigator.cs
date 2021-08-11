using System;
using System.Threading.Tasks;
using Plapp.Core;
using Plapp.Views.Pages;
using Xamarin.Forms;

namespace Plapp.Views.Infrastructure
{
    public class Navigator : INavigator
    {
        private readonly INavigation _navigation;
        private readonly IViewFactory _viewFactory;
        public Navigator(INavigation navigation, IViewFactory viewFactory)
        {
            _navigation = navigation;
            _viewFactory = viewFactory;
        }

        public async Task<IViewModel> GoBackAsync()
        {
            var view = await _navigation.PopAsync() as BaseContentPage<IViewModel>;

            return view.VM;
        }

        public async Task BackToRootAsync()
        {
            await _navigation.PopToRootAsync();
        }

        public async Task GoToAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IViewModel
        {
            var view = _viewFactory.CreatePage(setStateAction);
            await _navigation.PushAsync(view);
        }
        public async Task GoToAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            var view = _viewFactory.CreatePage(viewModel);
            await _navigation.PushAsync(view);
        }
    }
}
