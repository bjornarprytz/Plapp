using MaterialDesign.Icons;
using Plapp.UI.Constants;
using Xamarin.Forms;

namespace Plapp.UI.Extensions
{
    public static class ImageExtensions
    {
        public static T Aspect<T>(this T image, Aspect aspect)
            where T : Image
        {
            image.Aspect = aspect;
            return image;
        }
        
        public static T MaterialIcon<T>(this T image, MaterialIcon glyph, Color color = default)
            where T : Image
        {
            return image.Icon(glyph.ToIconFontString(), Fonts.MI, (double)IconSize.Medium, color);
        }

        public static T MaterialIcon<T>(this T image, MaterialIcon glyph, IconSize size, Color color = default)
            where T : Image
        {
            return image.Icon(glyph.ToIconFontString(), Fonts.MI, (double)size, color);
        }
        
        
        private static T Icon<T>(this T image, string glyph, string fontFamily, double size, Color color = default)
            where T : Image
        {
            image.Source = new FontImageSource
            {
                Glyph = glyph,
                FontFamily = fontFamily,
                Size = size,
                Color = color,
            };

            return image;
        }
    }
}