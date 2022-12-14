/*
 * Created by SharpDevelop.
 * User: cberman
 * Date: 04/16/2013
 * Time: 09:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Globalization;
using System.Windows.Data;
using ICSharpCode.AvalonEdit.Document;
using miRobotEditor.EditorControl;
using miRobotEditor.EditorControl.Languages;

namespace miRobotEditor.ViewModels.Converters
{
    public sealed class ActiveEditorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is KukaViewModel)
                return value;


            if (value is DocumentModel)
            {
                return value;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var doc = value as KukaViewModel;
            if (doc != null)
            {
                return doc;
            }

            if (value is  IDocument)
                return value;

            return Binding.DoNothing;
        }
    }
}
