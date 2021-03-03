using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
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
