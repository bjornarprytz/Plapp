using Xamarin.Forms;

namespace Plapp
{
    public static class LayoutExtensions
    {
        public static T HeightRequest<T>(this T element, double heightRequest)
            where T : VisualElement
        {
            element.HeightRequest = heightRequest;

            return element;
        }
    }
}
