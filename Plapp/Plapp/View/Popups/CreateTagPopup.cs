using MaterialDesign.Icons;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class CreateTagPopup : BasePopupPage<ICreateViewModel<ITagViewModel>>
    {
        public CreateTagPopup(ICreateViewModel<ITagViewModel> vm, ILogger logger) : base(vm, logger)
        {
            Content = ViewHelpers.PopupFrame(
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new TagForm()
                            .BindContext(nameof(VM.Partial)),
                        new Button()
                            .MaterialIcon(MaterialIcon.Check)
                            .BindCommand(nameof(VM.ConfirmCommand))
                    }
                });
        }
    }
}
