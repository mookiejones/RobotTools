using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace miRobotEditor.Core.Converters
{
    [Localizable(false),ValueConversion(typeof(object), typeof(string))]
    public class DoubleToStringConverter :IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            return value == null ? null :System.Convert.ToDouble(value).ToString(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
            }
            return System.Convert.ToDouble(String.Format("{0:F4}",System.Convert.ToDouble(value)));
        }

        #endregion
    }
}
