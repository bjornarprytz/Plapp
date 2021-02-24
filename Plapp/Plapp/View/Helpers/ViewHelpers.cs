using MaterialDesign.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;


namespace Plapp
{
    public static class ViewHelpers
    {
        public static Button FloatingActionButton()
            => new Button()
            .MaterialIcon(MaterialIcon.Add)
            .Circle(80);

        /*
         * The PhotoFrame a button to take a photo if there isn't one already.
         */
        public static Grid PhotoFrame(string imageBinding, string isVisibleBinding, string takePhotoCommandBinding)
            => new Grid
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
                .HorizontalOptions(LayoutOptions.FillAndExpand);

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
                    expanderIcon.HorizontalOptions(LayoutOptions.End)
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
    }
}
