using Microsoft.Extensions.Logging;
using Plapp.Core;
using Plapp.ViewModels;
using Xamarin.Forms;

namespace Plapp
{
    public abstract class BaseDataTemplateSelector<TViewModel> : DataTemplateSelector
        where TViewModel : IViewModel
    {
        protected static DataTemplate InvalidTemplate { get; set; } // TODO: Make this template something like a red X

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is not TViewModel)
            {
                IoC.Get<ILogger>().LogWarning($"Provided item was not a view model, but of type: {item?.GetType()}");
                return InvalidTemplate;
            }

            return OnSelectTypedTemplate((TViewModel)item, container);
        }

        protected abstract DataTemplate OnSelectTypedTemplate(TViewModel viewModel, BindableObject container);
    }
}
