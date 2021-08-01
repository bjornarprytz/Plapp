using Xamarin.Forms;

namespace Plapp.Views.Extensions.UI
{
    public static class LabelExtensions
    {
        public static T LineBreakMode<T>(this T label, LineBreakMode mode)
            where T : Label
        {
            label.LineBreakMode = mode;

            return label;
        }
    }
}
