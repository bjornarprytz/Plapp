using Xamarin.Forms;

namespace Plapp
{
    public class BasePage<VM> : ContentPage
        where VM : BaseViewModel
    {
        public VM ViewModel { get; private set; } 

        protected BasePage()
        {
            ViewModel = ViewModelLocator.Get<VM>();
            BindingContext = ViewModel;
        }
    }
}
