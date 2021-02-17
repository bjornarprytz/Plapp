using Plapp.Core;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Plapp
{
    public class DataSeriesInfoCard : BaseContentView<IDataSeriesViewModel>
    {
        public DataSeriesInfoCard()
        {
            Content = new Expander
            {
                Header = new DataSeriesHeader().BindContext(nameof(VM.Tag)),

                Content = new DataSeriesGraph().BindContext(".")
            };
        }
    }
}
