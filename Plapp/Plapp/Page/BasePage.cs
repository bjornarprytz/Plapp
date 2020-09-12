using Xamarin.Forms;

namespace Plapp
{
    public class BasePage<VM> : ContentPage
        where VM : BaseViewModel
    {
        public VM ViewModel => IoC.Get<VM>();

        protected BasePage()
        {
            BindingContext = ViewModel;
        }
    }
}
