using System;
using System.Globalization;
using System.Windows.Data;
using miRobotEditor.Core.Enums;

namespace miRobotEditor.ViewModels
{
    public sealed class EnumtoInt32 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Int32)(CartesianEnum)value;
            // Do the conversion from bool to visibility
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (CartesianEnum)Enum.Parse(typeof(CartesianEnum), ((Int32)value).ToString(CultureInfo.InvariantCulture));
        }
    }
}