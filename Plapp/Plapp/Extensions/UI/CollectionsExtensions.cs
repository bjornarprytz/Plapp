using Xamarin.Forms;

namespace Plapp
{
    public static class CollectionsExtensions
    {
        public static T DataTemplate<T>(this T collection, DataTemplate dataTemplate)
            where T : ItemsView
        {
            collection.ItemTemplate = dataTemplate;

            return collection;
        }
    }
}
