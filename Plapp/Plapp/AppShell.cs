using Plapp.Views.Pages;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp
{
    public class AppShell : Shell
    {
        public AppShell()
        {
            
            
            Routing.RegisterRoute("index", typeof(MainPage));
        }
    }
}