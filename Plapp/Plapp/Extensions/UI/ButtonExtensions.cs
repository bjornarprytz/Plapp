using Xamarin.Forms;

namespace Plapp
{
    public static class ButtonExtensions
    {
        public static T Icon<T>(this T button, string glyph, string fontFamily, NamedSize size=default, Color color=default)
            where T : Button
        {
            button.ImageSource = new FontImageSource
            {
                Glyph = glyph,
                FontFamily = fontFamily,
                Size = Device.GetNamedSize(size, button),
                Color = color
            };

            return button;
        }
    }
}
