using MaterialDesign.Icons;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class CreateDataPointsPopup : BasePopupPage<ICreateMultipleViewModel<IDataPointViewModel>>
    {
        public CreateDataPointsPopup()
        {
            Content = ViewHelpers.PopupFrame(
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new DataPointEditor()
                            .BindContext(nameof(VM.Current)),

                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new Button()
                                    .MaterialIcon(MaterialIcon.Undo)
                                    .BindCommand(nameof(VM.BackToPreviousCommand)),
                                new Button()
                                    .MaterialIcon(MaterialIcon.Check)
                                    .BindCommand(nameof(VM.ConfirmCommand)),
                                new Button()
                                    .MaterialIcon(MaterialIcon.Add)
                                    .BindCommand(nameof(VM.ConfirmCurrentCommand))
                            }
                        }
                    }
                });
        }
    }
}
