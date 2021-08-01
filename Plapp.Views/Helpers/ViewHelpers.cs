using System;
using MaterialDesign.Icons;
using Plapp.Views.Extensions.UI;
using Plapp.Views.Styles;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Plapp.Views.Helpers
{
    public static class ViewHelpers
    {
        public static Button FloatingActionButton()
            => new Button()
            .Margin(new Thickness(20))
            .Circle(80);

        /*
         * The PhotoFrame a button to take a photo if there isn't one already.
         */
        public static Grid PhotoFrame(string imageBinding, string isVisibleBinding, string takePhotoCommandBinding)
            => new Grid()
            {
                Children =
                {
                    new Image()
                        .Bind(imageBinding),

                    new Button()
                        .MaterialIcon(MaterialIcon.AddAPhoto, IconSize.Huge)
                        .Bind(VisualElement.IsVisibleProperty, isVisibleBinding)
                        .BindCommand(takePhotoCommandBinding)
                }
            };

        public static Expander ExpanderWithHeader(View headerDecor)
        {
            var expander = new Expander()
                .FillHorizontal();

            var expanderIcon = new Image
            {
                Source = Icon(MaterialIcon.ExpandMore)
            }.BindTrigger(
                Image.SourceProperty,
                Icon(MaterialIcon.ExpandLess),
                expander,
                nameof(Expander.IsExpanded),
                true);

            expander.Header = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,

                Children = {
                    headerDecor,
                    expanderIcon.HorizontalOptions(LayoutOptions.EndAndExpand)
                }
            };

            return expander;
        }

        public static FontImageSource Icon(MaterialIcon icon, Color color = default)
        {
            return new FontImageSource
            {
                Glyph = icon.ToIconFontString(),
                FontFamily = Fonts.MI,
                Size = (double)IconSize.Medium,
                Color = color,
            };
        }

        public static Picker EnumPicker<TEnum>(string title = null) where TEnum : Enum
        {
            var values = Enum.GetNames(typeof(TEnum));

            var picker = new Picker();

            foreach (var val in values)
            {
                picker.Items.Add(val);
            }

            picker.Title = title ?? $"Pick a {typeof(TEnum)}";

            return picker;
        }

        public static Frame PopupFrame(View content)
        {
            return new Frame
            {
                CornerRadius = 20,
                Padding = 30,
                Margin = new Thickness(20, 40),
                BackgroundColor = Palette.BackgroundLight,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                Content = content
            };
        }
    }
}
