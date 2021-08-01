using Plapp;
using Plapp.Core;
using Plapp.Views.Extensions.UI;
using Plapp.Views.Helpers;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp.Views.Components
{
    public class DataSeriesInfoCard : BaseContentView<IDataSeriesViewModel>
    {
        public DataSeriesInfoCard()
        {
            var expander = ViewHelpers.ExpanderWithHeader(
                new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new TagBadge().BindContext(nameof(VM.Tag)),
                        new Label().Bind(nameof(VM.Title))
                    }
                }).BindContext();

            expander.Content = new DataSeriesGraph()
                .BindTapGesture(nameof(VM.OpenCommand))
                .FillVertical()
                .BindContext();

            Content = expander;
        }
    }
}
