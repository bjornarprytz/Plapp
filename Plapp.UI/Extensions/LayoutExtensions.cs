using System;
using Xamarin.Forms;

namespace Plapp.UI.Extensions
{
    public static class LayoutExtensions
    {
        public static T VerticalOptions<T>(this T view, LayoutOptions layout)
            where T : View
        {
            view.VerticalOptions = layout;
            return view;
        }
        
        public static T HorizontalOptions<T>(this T view, LayoutOptions layout)
            where T : View
        {
            view.HorizontalOptions = layout;
            return view;
        }
        
        public static T VerticalAndHorizontalOptions<T>(this T view, LayoutOptions layout)
            where T : View
        {
            return view
                .VerticalOptions(layout)
                .HorizontalOptions(layout);
        }
        
        public static T HeightRequest<T>(this T element, double heightRequest)
            where T : VisualElement
        {
            element.HeightRequest = heightRequest;

            return element;
        }

        public static T  ItemTemplate<T>(this T itemsView, DataTemplate itemTemplate)
            where T : ItemsView
        {
            itemsView.ItemTemplate = itemTemplate;

            return itemsView;
        }
        
        public static T ItemTemplate<T>(this T itemsView, Func<object> templateSelector)
            where T : ItemsView
        {
            itemsView.ItemTemplate = new DataTemplate(templateSelector);

            return itemsView;
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