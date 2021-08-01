using Plapp.Views.Styles;
using Xamarin.Forms;

namespace Plapp.Views.Converters
{
    public class StringToColorConverter : BaseValueConverter<string, Color>
    {
        protected override Color Convert(string from)
        {
            if (from == null)
            {
                return Palette.Information;
            }

            return Color.FromHex(from);
        }

        protected override string ConvertBack(Color from)
        {
            return from.ToHex();
        }
    }
}
