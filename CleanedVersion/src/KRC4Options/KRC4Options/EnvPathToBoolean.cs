using System;
using System.Globalization;
using System.Windows.Data;

namespace KRC4Options
{
    public sealed class EnvPathToBoolean : IValueConverter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value.ToString().Equals("Cabinet/", StringComparison.OrdinalIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? "Office/" : "Cabinet/";
        }
    }
}