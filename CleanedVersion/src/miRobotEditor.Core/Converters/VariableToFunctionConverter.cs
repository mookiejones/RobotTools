using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.Core.Converters
{
    public sealed class VariableToFunctionConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReadOnlyObservableCollection<IVariable>)
            {
                var prev = value as ReadOnlyObservableCollection<IVariable>;

                
              // If Count is zero, hide the function window
                if (prev.Count == 0)
                {
                    return new ObservableCollection<IVariable>();
                }
                var list = new ObservableCollection<IVariable>();
               

                foreach (var i in prev.Where(i => i.Type == "def"))
                    list.Add(i);

                var result = new ReadOnlyObservableCollection<IVariable>(list);


                return result;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
