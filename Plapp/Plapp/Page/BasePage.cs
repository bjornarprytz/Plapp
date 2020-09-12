using Xamarin.Forms;

namespace Plapp
{
    public class BasePage<VM> : ContentPage
        where VM : BaseViewModel, new()
    {
        public VM ViewModel => new VM(); // TODO: Dependency Injection

        protected BasePage()
        {
            BindingContext = ViewModel;
        }
    }
}
