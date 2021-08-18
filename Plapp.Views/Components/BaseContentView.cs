using Plapp.Core;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.Views.Components
{
    public abstract class BaseContentView<TViewModel> : ReactiveContentView<TViewModel>
        where TViewModel : class, IViewModel
    {
        public TViewModel VM { get { return (TViewModel)BindingContext; } set { BindingContext = value; } }

    }
}
