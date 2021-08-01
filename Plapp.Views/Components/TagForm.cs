using Plapp;
using Plapp.Core;
using Plapp.Views.Helpers;
using Xamarin.Forms;

namespace Plapp.Views.Components
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
                    new Entry().Bind(nameof(VM.Key)),
                    new Entry().Bind(nameof(VM.Unit)),
                    ViewHelpers.EnumPicker<DataType>().Bind(Picker.SelectedItemProperty, nameof(VM.DataType)),

                    // TODO: Color picker?
                }
            };
        }
    }
}
