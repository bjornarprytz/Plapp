using Plapp.Core;
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
                    new Entry().Bind(nameof(VM.Key)),
                    new Entry().Bind(nameof(VM.Unit)),
                    ViewHelpers.EnumPicker<DataType>().Bind(Picker.SelectedItemProperty, nameof(VM.DataType)),

                    // TODO: Color picker?
                }
            };
        }
    }
}
