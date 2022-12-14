using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core.Classes.Messaging;

namespace miRobotEditor.Core.Converters
{
    public class GetFileIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                switch (Path.GetExtension(value.ToString().ToLower()))
                {
                    case ".src":
                        var ico =  Utilities.LoadBitmap(Global.ImgSrc);
                        return ico;
                    case ".dat":
                        return Utilities.LoadBitmap(Global.ImgDat);
                    case ".sub":
                        return Utilities.LoadBitmap(Global.ImgSps);
                }
                

            }
            catch(Exception ex )
            {
                var msg = new ErrorMessage("Convert", ex);
                Messenger.Default.Send(msg);

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}