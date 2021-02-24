using Plapp.Core;
using Xamarin.Forms;

namespace Plapp
{
    public class CreateTagPopup : BasePopupPage<ICreateViewModel<ITagViewModel>>
    {
        public CreateTagPopup()
        {
            Content = new StackLayout // TODO: Remove StackLayout or add more Children (OK button?)
            {
                Orientation = StackOrientation.Vertical,

                Children =
                { 
                    new TagForm().BindContext(nameof(VM.UnderCreation))
                }
            };
        }
    }
}
