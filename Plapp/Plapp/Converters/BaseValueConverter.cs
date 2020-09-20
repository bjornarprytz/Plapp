using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plapp.Converters
{
    public abstract class BaseValueConverter<T> : IMarkupExtension<T>, IValueConverter
        where T : BaseValueConverter<T>, new()
    {
        private static T Converter = null;

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public T ProvideValue(IServiceProvider serviceProvider)
        {
            return Converter ?? new T();
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return Converter ?? new T();
        }
    }
}
