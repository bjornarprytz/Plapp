using Plapp.Core;
using Xamarin.CommunityToolkit.UI.Views;

namespace Plapp
{
    public class DataSeriesInfoCard : BaseContentView<IDataSeriesViewModel>
    {
        public DataSeriesInfoCard()
        {
            Content = new Expander
            {
                Header = new DataSeriesHeader().BindContext(),
                Content = new DataSeriesGraph().BindContext()
            };
        }
    }
}
