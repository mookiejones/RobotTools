using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace miRobotEditor.Core.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        [Localizable(false)]
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                var visible = System.Convert.ToBoolean(value, culture);

                if (InvertVisibility)
                    visible = !visible;
                return visible ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
           // throw new InvalidOperationException("Converter cannot convert back.");
            return ((Visibility)value)==Visibility.Visible;

        }

        public Boolean InvertVisibility { get; set; }

    }

}
