using System.Windows;
using miRobotEditor.Core.Classes;

namespace miRobotEditor.Core
{
    public sealed class ShiftModel:DependencyObject
    {


        public CartesianPosition OldValues
        {
            get { return (CartesianPosition)GetValue(OldValuesProperty); }
            set { SetValue(OldValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OldValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldValuesProperty =
            DependencyProperty.Register("OldValues", typeof(CartesianPosition), typeof(ShiftModel), new PropertyMetadata(new CartesianPosition()));



        public CartesianPosition NewValues
        {
            get { return (CartesianPosition)GetValue(NewValuesProperty); }
            set { SetValue(NewValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewValuesProperty =
            DependencyProperty.Register("NewValues", typeof(CartesianPosition), typeof(ShiftModel), new PropertyMetadata(new CartesianPosition()));




        public CartesianPosition DiffValues
        {
            get { return (CartesianPosition)GetValue(DiffValuesProperty); }
            set { SetValue(DiffValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DiffValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiffValuesProperty =
            DependencyProperty.Register("DiffValues", typeof(CartesianPosition), typeof(ShiftModel), new PropertyMetadata(new CartesianPosition()));

        

        

        
    }
}
