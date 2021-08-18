using Plapp.Views.Pages;
using Xamarin.Forms;

namespace Plapp
{
    public class AppShell : Shell
    {
        public AppShell()
        {
            SetTabBarIsVisible(this, false);
            Routing.RegisterRoute("loading", typeof(LoadingPage));
        }
    }
}