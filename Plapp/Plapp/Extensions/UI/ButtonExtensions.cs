﻿using MaterialDesign.Icons;
using Xamarin.Forms;

namespace Plapp
{
    public static class ButtonExtensions
    {
        public static T Circle<T>(this T button, double diameter)
            where T : Button
        {
            button.HeightRequest = diameter;
            button.WidthRequest = diameter;
            button.CornerRadius = (int)(diameter / 2);

            return button;
        }

        public static T Icon<T>(this T button, string glyph, string fontFamily, Color color=default)
            where T : Button
        {
            button.ImageSource = new FontImageSource
            {
                Glyph = glyph,
                FontFamily = fontFamily,
                Color = color,
            };

            return button;
        }

        public static T Icon<T>(this T button, string glyph, string fontFamily, double size, Color color = default)
            where T : Button
        {
            button.ImageSource = new FontImageSource
            {
                Glyph = glyph,
                FontFamily = fontFamily,
                Size = size,
                Color = color,
            };

            return button;
        }

        public static T MaterialIcon<T>(this T button, MaterialIcon glyph, Color color = default)
            where T : Button
        {
            return button.Icon(glyph.ToIconFontString(), Fonts.MI, (double)IconSize.Medium, color);
        }

        public static T MaterialIcon<T>(this T button, MaterialIcon glyph, IconSize size, Color color = default)
            where T : Button
        {
            return button.Icon(glyph.ToIconFontString(), Fonts.MI, (double)size, color);
        }
    }
}
