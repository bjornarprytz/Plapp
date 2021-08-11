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
    public class CreateDataPointsPopup : BasePopupPage<ICreateMultipleViewModel<IDataPointViewModel>>
    {
        public CreateDataPointsPopup(ICreateMultipleViewModel<IDataPointViewModel> viewModel, ILogger logger) : base(viewModel, logger)
        {
            Content = ViewHelpers.PopupFrame(
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new DataPointEditor()
                            .BindContext(nameof(ViewModel.Current)),

                        new Slider
                        { 
                            Maximum = 100, // Placeholder
                            Margin = new Thickness(0, 40),
                        }
                        .Bind(BindingHelpers.BuildPath(nameof(ViewModel.Current), nameof(ViewModel.Current.Value)))
                        .Bind(Slider.DragCompletedCommandProperty, nameof(ViewModel.ConfirmCurrentCommand)),

                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new Button()
                                    .HorizontalOptions(LayoutOptions.Start)
                                    .MaterialIcon(MaterialIcon.Undo)
                                    .BindCommand(nameof(ViewModel.BackToPreviousCommand)),
                                new Button()
                                    .HorizontalOptions(LayoutOptions.End)
                                    .MaterialIcon(MaterialIcon.Check)
                                    .BindCommand(nameof(ViewModel.ConfirmCommand)),
                            }
                        }
                    }
                });
        }
    }
}
