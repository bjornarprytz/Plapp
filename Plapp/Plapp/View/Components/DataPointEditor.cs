using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class DataPointEditor : BaseContentView<IDataPointViewModel>
    {
        public DataPointEditor()
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new DatePicker().Bind(nameof(VM.Date)),
                    new Entry().Numeric().Bind(nameof(VM.Value)),
                }
            };
        }
    }
}
