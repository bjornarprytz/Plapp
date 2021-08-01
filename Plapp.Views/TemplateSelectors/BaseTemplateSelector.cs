using Plapp.Core;
using Xamarin.Forms;

namespace Plapp.Views.TemplateSelectors
{
    public abstract class BaseTemplateSelector<TViewModel> : DataTemplateSelector
        where TViewModel : IViewModel
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (! (item is TViewModel))
            {
                return OnInvalidTemplate();
            }

            return OnSelectTemplate((TViewModel)item);
        }
        protected virtual DataTemplate OnInvalidTemplate() => null;
        protected abstract DataTemplate OnSelectTemplate(TViewModel vm);
    }
}
