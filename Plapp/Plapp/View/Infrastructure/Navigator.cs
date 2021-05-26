using Plapp.Core;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plapp
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

        public async Task<IIOViewModel> GoBackAsync()
        {
            var view = await _navigation.PopAsync() as BaseContentPage<IIOViewModel>;

            return view.VM;
        }

        public async Task BackToRootAsync()
        {
            await _navigation.PopToRootAsync();
        }

        public async Task GoToAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IIOViewModel
        {
            var view = _viewFactory.CreatePage(setStateAction);
            await _navigation.PushAsync(view);
        }
        public async Task GoToAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IIOViewModel
        {
            var view = _viewFactory.CreatePage(viewModel);
            await _navigation.PushAsync(view);
        }
    }
}
