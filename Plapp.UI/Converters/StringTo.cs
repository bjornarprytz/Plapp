using System;
using Xamarin.Forms;

namespace Plapp.UI.Converters
{
    public static class StringTo
    {
        public static ImageSource ImageSource(string? arg)
        {
            return new FileImageSource{ File = arg };
        }

        public static T Enum<T>(string? arg)
            where T : Enum
        {
            if (arg == null)
                return default;
            
            return (T)System.Enum.Parse(typeof(T), arg);
        }
    }
}