using Plapp.Core;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plapp
{
    public class Navigator : INavigator
    {
        private INavigation Navigation => IoC.Get<INavigation>();
        private IViewFactory ViewFactory => IoC.Get<IViewFactory>();

        public async Task<IRootViewModel> GoBackAsync()
        {
            var view = await Navigation.PopAsync() as BaseContentPage<IRootViewModel>;

            return view.VM;
        }

        public async Task BackToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task GoToAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : IRootViewModel
        {
            var view = ViewFactory.CreatePage(setStateAction);
            await Navigation.PushAsync(view);
        }
        public async Task GoToAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : IRootViewModel
        {
            var view = ViewFactory.CreatePage(viewModel);
            await Navigation.PushAsync(view);
        }
    }
}
