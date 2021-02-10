using Plapp.Core;
using System;
using System.Threading.Tasks;

namespace Plapp
{
    public static class NavigationHelpers
    {
        public static async Task NavigateTo<TViewModel>(Action<TViewModel> setStateAction=null) 
            where TViewModel : IViewModel
        {
            await IoC.Get<INavigator>().PushAsync(setStateAction);
        }

        public static async Task NavigateTo<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            await IoC.Get<INavigator>().PushAsync(viewModel);
        }

        public static async Task Back()
        {
            await IoC.Get<INavigator>().PopAsync();
        }
    }
}
