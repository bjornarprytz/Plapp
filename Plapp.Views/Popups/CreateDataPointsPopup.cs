using Plapp.Core;
using Plapp.Views.Extensions.UI;
using Plapp.Views.Helpers;
using Xamarin.Forms;

namespace Plapp.Views.Popups
{
    public class CreateDataPointsPopup : BasePopupPage<ICreateMultipleViewModel<IDataPointViewModel>>
    {
        public CreateDataPointsPopup(ICreateMultipleViewModel<IDataPointViewModel> vm, ILogger logger) : base(vm, logger)
        {
            Content = ViewHelpers.PopupFrame(
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new DataPointEditor()
                            .BindContext(nameof(VM.Current)),

                        new Slider
                        { 
                            Maximum = 100, // Placeholder
                            Margin = new Thickness(0, 40),
                        }
                        .Bind(BindingHelpers.BuildPath(nameof(VM.Current), nameof(VM.Current.Value)))
                        .Bind(Slider.DragCompletedCommandProperty, nameof(VM.ConfirmCurrentCommand)),

                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new Button()
                                    .HorizontalOptions(LayoutOptions.Start)
                                    .MaterialIcon(MaterialIcon.Undo)
                                    .BindCommand(nameof(VM.BackToPreviousCommand)),
                                new Button()
                                    .HorizontalOptions(LayoutOptions.End)
                                    .MaterialIcon(MaterialIcon.Check)
                                    .BindCommand(nameof(VM.ConfirmCommand)),
                            }
                        }
                    }
                });
        }
    }
}
