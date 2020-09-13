using System.Threading.Tasks;

namespace Plapp.Extensions
{
    public static class NavigationHelpers
    {
        public static async Task NavigateTo<VM>() where VM : class, IViewModel
        {
            await ServiceLocator.Get<INavigator>().PushAsync<VM>();
        }
    }
}
