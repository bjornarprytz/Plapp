using System;
using System.Globalization;
using Xamarin.Forms;

namespace Plapp
{

    public abstract class BaseValueConverter<TFrom, TTo> : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not TFrom)
            {
                throw new ArgumentException($"Convert expected {typeof(TFrom)}, got {value?.GetType()}");
            }

            return Convert((TFrom)value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not TTo)
            {
                throw new ArgumentException($"ConvertBack expected {typeof(TTo)}, got {value?.GetType()}");
            }

            return ConvertBack((TTo)value);
        }

        protected abstract TTo Convert(TFrom from);
        protected virtual TFrom ConvertBack(TTo from)
        {
            throw new NotImplementedException();
        }
    }
    
    public abstract class BaseValueConverter : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
