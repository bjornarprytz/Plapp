using Plapp.Core;
using Xamarin.Forms;

namespace Plapp
{
    public class CreateTagPopup : BasePopupPage<ICreateViewModel<ITagViewModel>>
    {
        public CreateTagPopup()
        {
            Content = ViewHelpers.PopupFrame(new TagForm().BindContext(nameof(VM.UnderCreation)));
        }
    }
}
