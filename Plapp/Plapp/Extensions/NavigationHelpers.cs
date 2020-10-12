using Plapp.Core;
using System;
using System.Threading.Tasks;

namespace Plapp
{
    public static class NavigationHelpers
    {
        public static async Task NavigateTo<TViewModel>(Action<TViewModel> setStateAction=null) 
            where TViewModel : class, IViewModel
        {
            await IoC.Get<INavigator>().PushAsync(setStateAction);
        }

        public static async Task NavigateTo<TViewModel>(TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            await IoC.Get<INavigator>().PushAsync(viewModel, setStateAction);
        }

        public static async Task PushPopup<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            await IoC.Get<INavigator>().PushModalAsync(setStateAction);
        }

        public static async Task PushPopup<TViewModel>(TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            await IoC.Get<INavigator>().PushModalAsync(viewModel, setStateAction);
        }

        public static async Task<IViewModel> PopPopup()
        {
            return await IoC.Get<INavigator>().PopModalAsync();
        }
    }
}
