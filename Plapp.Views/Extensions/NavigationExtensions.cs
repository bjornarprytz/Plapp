using System.Linq;
using Xamarin.Forms;

namespace Plapp.Views.Extensions
{
    public static class NavigationExtensions
    {
        public static Page CurrentPage(this INavigation navigation)
        {
            return navigation.NavigationStack.LastOrDefault();
        }

    }
}
