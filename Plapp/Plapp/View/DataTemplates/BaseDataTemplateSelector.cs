using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plapp
{
    public abstract class BaseDataTemplateSelector<TViewModel> : DataTemplateSelector
        where TViewModel : BaseViewModel
    {
        protected static DataTemplate InvalidTemplate { get; set; } // TODO: Make this template something like a red X

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is not TViewModel)
            {
                // TODO: Log something here
                return InvalidTemplate;
            }

            return OnSelectTypedTemplate((TViewModel)item, container);
        }

        protected abstract DataTemplate OnSelectTypedTemplate(TViewModel viewModel, BindableObject container);
    }
}
