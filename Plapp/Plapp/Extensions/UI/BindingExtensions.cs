using Xamarin.Forms;

namespace Plapp
{
    public static class BindingExtensions
    {
        public static T BindItems<T>(this T collectionView, string name, DataTemplate template)
                where T : ItemsView
        {
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, name);
            collectionView.ItemTemplate = template;

            return collectionView;
        }
    }
}
