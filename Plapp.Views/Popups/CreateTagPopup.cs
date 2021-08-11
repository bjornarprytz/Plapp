using MaterialDesign.Icons;
using Microsoft.Extensions.Logging;
using Plapp.Core;
using Plapp.Views.Components;
using Plapp.Views.Extensions.UI;
using Plapp.Views.Helpers;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp.Views.Popups
{
    public class CreateTagPopup : BasePopupPage<ICreateViewModel<ITagViewModel>>
    {
        public CreateTagPopup(ICreateViewModel<ITagViewModel> viewModel, ILogger logger) : base(viewModel, logger)
        {
            Content = ViewHelpers.PopupFrame(
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new TagForm()
                            .BindContext(nameof(ViewModel.Partial)),
                        new Button()
                            .MaterialIcon(MaterialIcon.Check)
                            .BindCommand(nameof(ViewModel.ConfirmCommand))
                    }
                });
        }
    }
}
