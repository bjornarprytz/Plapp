using Plapp.Converters;
using System;
using System.Globalization;

namespace Plapp
{
    public class DateTimeToDateStringConverter : BaseValueConverter<DateTimeToDateStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                return null;
            }

            return ((DateTime)value).ToShortDateString();
        }
    }
}
