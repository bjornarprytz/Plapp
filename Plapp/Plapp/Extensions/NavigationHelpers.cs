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
    }
}
