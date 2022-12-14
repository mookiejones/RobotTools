using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using miRobotEditor.Core.Enums;

namespace miRobotEditor.ViewModels
{
    [Localizable(false)]
    public class AngleToolTipConverter : IValueConverter
    {
        string _title = string.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((CartesianEnum)value)
            {
                case CartesianEnum.ABB_Quaternion:
                    _title = "ABB Quaternion";
                    switch (parameter.ToString())
                    {
                        case "V1":
                            return _title + " Q1";
                        case "V2":
                            return _title + " Q2";
                        case "V3":
                            return _title + " Q3";
                        case "V4":
                            return _title + " Q4";
                    }
                    break;
                case CartesianEnum.Alpha_Beta_Gamma:
                    return "Alpha Beta Gamma";
                case CartesianEnum.Axis_Angle:
                    return "Axis Angle";
                case CartesianEnum.Euler_ZYZ:
                    return "Euler ZYZ";
                case CartesianEnum.Kuka_ABC:
                    _title = "Kuka ABC";
                    switch (parameter.ToString())
                    {
                        case "V1":
                            return _title + " A. Rotation around Z.";
                        case "V2":           
                            return _title + " B. Rotation around Y.";
                        case "V3":           
                            return _title + " C. Rotation around X.";
                    }
                    break;
                case CartesianEnum.Roll_Pitch_Yaw:
                    _title = "Roll Pitch Yaw";
                    switch (parameter.ToString())
                    {
                        case "V1":
                            return _title + " R. Rotation around X.";
                        case "V2":           
                            return _title + " P. Rotation around Y.";
                        case "V3":           
                            return _title + " Y. Rotation around Z.";
                    }
                    break;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}