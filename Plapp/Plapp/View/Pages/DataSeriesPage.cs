using Plapp.Core;
using Xamarin.Forms;

namespace Plapp
{
    public class DataSeriesPage : BaseContentPage<IDataSeriesViewModel>
    {
        public DataSeriesPage()
        {
            Content = new StackLayout
            {
                Children =
                {
                    new DataSeriesGraph().BindContext(),
                    new TagBadge().BindContext(nameof(VM.Tag)),
                    new Label { Text = "Put slider here which can add points to the series" }
                }
            };
        }
    }
}
