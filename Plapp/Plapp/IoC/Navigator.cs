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

        public async Task<IViewModel> PopAsync()
        {
            Page view = await Navigation.PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync() => await PopAsync();

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task PushAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            var view = ViewFactory.CreateView(setStateAction);

            await Navigation.PushAsync(view);
        }

        public async Task PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            var view = ViewFactory.CreateView(setStateAction);

            await Navigation.PushModalAsync(view);
        }
    }
}
