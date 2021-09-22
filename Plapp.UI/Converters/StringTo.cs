using Xamarin.Forms;

namespace Plapp.UI.Converters
{
    public static class StringTo
    {
        public static ImageSource ImageSource(string? arg)
        {
            return new FileImageSource{ File = arg };
        }
    }
}