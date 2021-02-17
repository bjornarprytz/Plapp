using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class DataSeriesHeader : BaseContentView<ITagViewModel>
    {
        public DataSeriesHeader()
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label
                    {
                        
                    }.Bind(nameof(VM.Id))
                    .BackgroundColor(Color.Pink),
                }
            };
        }


    }
}
