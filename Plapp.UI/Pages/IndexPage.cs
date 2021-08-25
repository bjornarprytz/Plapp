using Plapp.Core;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class IndexPage : ReactiveContentPage<IApplicationViewModel>
    {
        public IndexPage()
        {
            Content = new Button
            {
                Text = "hello world"
            };
        }
    }
}