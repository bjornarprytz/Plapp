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

        public static T ItemsLayout<T>(this T structuredElement, IItemsLayout itemsLayout)
            where T : StructuredItemsView
        {
            structuredElement.ItemsLayout = itemsLayout;

            return structuredElement;
        }
        
        public static T ItemSizingStrategy<T>(this T structuredElement, ItemSizingStrategy itemSizingStrategy)
            where T : StructuredItemsView
        {
            structuredElement.ItemSizingStrategy = itemSizingStrategy;

            return structuredElement;
        }
    }
}
