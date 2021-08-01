using Xamarin.Forms;

namespace Plapp.Views.Extensions.UI
{
    public static class ImageExtensions
    {
        public static T Aspect<T>(this T image, Aspect aspect)
            where T : Image
        {
            image.Aspect = aspect;
            return image;
        }
    }
}
