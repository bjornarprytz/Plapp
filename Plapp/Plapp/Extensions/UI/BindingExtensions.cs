using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public static class BindingExtensions
    {
        public static T BindItems<T>(this T collectionView, string path, DataTemplate template)
                where T : ItemsView
        {
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, path);
            collectionView.ItemTemplate = template;

            return collectionView;
        }

        public static T BindContext<T>(this T bindableObject, string path = ".")
            where T : BindableObject
        {
            return bindableObject.Bind(BindableObject.BindingContextProperty, path);
        }

        public static T BindContext<T>(this T bindableObject, string path, IValueConverter valueConverter)
            where T : BindableObject
        {
            return bindableObject.Bind(BindableObject.BindingContextProperty, path, converter: valueConverter);
        }
    }
}
