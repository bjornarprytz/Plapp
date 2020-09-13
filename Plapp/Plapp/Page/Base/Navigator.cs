using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plapp
{
    public class Navigator : INavigator
    {
        private INavigation Navigation => ServiceLocator.Get<INavigation>();

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

        public async Task<VM> PushAsync<VM>(Action<VM> setStateAction = null)
            where VM : class, IViewModel
        {
            var view = ViewFactory.Resolve(setStateAction);
            await Navigation.PushAsync(view);
            return ViewModelLocator.Get<VM>();
        }

        public async Task<VM> PushAsync<VM>(VM viewModel)
            where VM : class, IViewModel
        {
            var view = ViewFactory.Resolve(viewModel);
            await Navigation.PushAsync(view);
            return viewModel;
        }

        public async Task<VM> PushModalAsync<VM>(Action<VM> setStateAction = null)
            where VM : class, IViewModel
        {
            var viewModel = ViewModelLocator.Get<VM>();
            var view = ViewFactory.Resolve(viewModel, setStateAction);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }

        public async Task<VM> PushModalAsync<VM>(VM viewModel)
            where VM : class, IViewModel
        {
            var view = ViewFactory.Resolve(viewModel);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }
    }
}
