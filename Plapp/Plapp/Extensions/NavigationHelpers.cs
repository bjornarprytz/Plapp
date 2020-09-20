using System;
using System.Threading.Tasks;

namespace Plapp.Extensions
{
    public static class NavigationHelpers
    {
        public static async Task NavigateTo<TViewModel>(Action<TViewModel> setStateAction=null) 
            where TViewModel : class, IViewModel
        {
            await IoC.Get<INavigator>().PushAsync(setStateAction);
        }
    }
}
