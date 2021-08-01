using Plapp.Core;
using Xamarin.Forms;

namespace Plapp.Views.TemplateSelectors
{
    public abstract class BaseTemplateSelector<TViewModel> : DataTemplateSelector
        where TViewModel : IViewModel
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (! (item is TViewModel model))
            {
                return OnInvalidTemplate();
            }

            return OnSelectTemplate(model);
        }
        protected virtual DataTemplate OnInvalidTemplate() => null;
        protected abstract DataTemplate OnSelectTemplate(TViewModel vm);
    }
}
