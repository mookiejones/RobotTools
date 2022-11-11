using ICSharpCode.AvalonEdit.Document;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RobotTools.UI.Editor.Converters
{
    public sealed class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value is KukaViewModel)
            //    return value;


            //if (value is DocumentModel)
            //{
            //    return value;
            //}

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var doc = value as KukaViewModel;
            //if (doc != null)
            //{
            //    return doc;
            //}

            if (value is IDocument)
                return value;

            return Binding.DoNothing;
        }
    }
}
