using Xamarin.Forms;

namespace Plapp
{
    public static class BindingExtensions
    {
        public static T BindItems<T>(this T collectionView, string name)
                where T : ItemsView
        {
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, name);

            return collectionView;
        }
    }
}
