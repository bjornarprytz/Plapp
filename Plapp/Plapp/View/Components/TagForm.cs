using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class TagForm : BaseContentView<ITagViewModel>
    {
        public TagForm()
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Entry().Bind(nameof(VM.Id)),
                    new Entry().Bind(nameof(VM.Unit)),
                    ViewHelpers.EnumPicker<DataType>().Bind(Picker.SelectedItemProperty, nameof(VM.DataType)),

                    // TODO: Color picker?
                }
            };
        }
    }
}
