using Xamarin.Forms;

namespace Plapp.UI.Extensions
{
    public static class LabelExtensions
    {
        public static T TextColor<T>(this T entry, Color color)
            where T : Label
        { 
            entry.TextColor = color; 
            return entry; 
        }
    }
}