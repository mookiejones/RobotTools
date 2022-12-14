using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace miRobotEditor.Core.Converters
{
    public class GetFileSystemInfosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var info = value as DirectoryInfo;
                if (info != null)
                {
                    return info.GetFileSystemInfos();
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch { }
// ReSharper restore EmptyGeneralCatchClause
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}