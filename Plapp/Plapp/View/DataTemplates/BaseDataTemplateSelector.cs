using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plapp
{
    public class BaseDataTemplateSelector<TViewModel> : DataTemplateSelector
        where TViewModel : BaseViewModel
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected virtual DataTemplate SelectTemplate(TViewModel viewModel)
        {
            return ValidTemplate;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is not TViewModel)
            {
                return InvalidTemplate;
            }

            return SelectTemplate((TViewModel)item);
        }
    }
}
