using Plapp.Core;
using Plapp.Views.Extensions.UI;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp.Views.Components
{
    public class DataSeriesHeader : BaseContentView<IDataSeriesViewModel>
    {
        public DataSeriesHeader()
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new TagBadge().BindContext(nameof(VM.Tag)),
                    new Label().Bind(nameof(VM.Title)),
                    // TODO: Expander icon
                }
            };
        }
    }
}
