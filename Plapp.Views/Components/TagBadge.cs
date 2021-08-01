using Plapp.Core;
using Plapp.Views.Converters;
using Xamarin.Forms;

namespace Plapp.Views.Components
{
    public class TagBadge : BaseContentView<ITagViewModel>
    {
        public TagBadge()
        {
            Content = new Label()
                        .Bind(nameof(VM.Key))
                        .Bind(BackgroundColorProperty, nameof(VM.Color), converter: new StringToColorConverter());
        }
    }
}
