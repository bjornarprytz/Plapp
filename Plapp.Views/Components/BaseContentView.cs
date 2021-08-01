using Plapp.Core;
using Xamarin.Forms;

namespace Plapp.Views.Components
{
    public abstract class BaseContentView<TViewModel> : ContentView
        where TViewModel : IViewModel
    {
        public TViewModel VM { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

    }
}
