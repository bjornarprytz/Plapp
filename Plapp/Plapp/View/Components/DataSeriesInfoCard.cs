using MaterialDesign.Icons;
using Plapp.Core;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Plapp
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
                .FillVertical()
                .BindContext();

            Content = expander;
        }
    }
}
