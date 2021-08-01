using Xamarin.Forms;

namespace Plapp.Views.Extensions.UI
{
    public static class ViewExtensions
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
    }
}
