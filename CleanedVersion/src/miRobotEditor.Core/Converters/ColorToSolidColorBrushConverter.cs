using System;
using System.Globalization;
using System.Windows.Data;

namespace miRobotEditor.Core.Converters
{
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
