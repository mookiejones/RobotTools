using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using miRobotEditor.Core;
using miRobotEditor.Core.Enums;

namespace miRobotEditor.UI.Controls
{
    /// <summary>
    ///     Interaction logic for ValueBox.xaml
    /// </summary>
    public sealed partial class ValueBox : UserControl
    {
        public ValueBox()
        {
            InitializeComponent();
        }
    }

    public sealed class ValueBoxModel : DependencyObject
    {
        public event ItemsChangedEventHandler ItemsChanged;
        void RaiseItemsChanged()
        {
            if (ItemsChanged != null)
                ItemsChanged(this,null);
        }
        #region Properties



        public double V1
        {
            get { return (double)GetValue(V1Property); }
            set
            {
                SetValue(V1Property, value); RaiseItemsChanged();
            }
        }

        // Using a DependencyProperty as the backing store for V1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty V1Property =
            DependencyProperty.Register("V1", typeof(double), typeof(ValueBoxModel), new PropertyMetadata(0.0));




        public double V2
        {
            get { return (double)GetValue(V2Property); }
            set
            {
                SetValue(V2Property, value); RaiseItemsChanged();
            }
        }

        // Using a DependencyProperty as the backing store for V2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty V2Property =
            DependencyProperty.Register("V2", typeof(double), typeof(ValueBoxModel), new PropertyMetadata(0.0));




        public double V3
        {
            get { return (double)GetValue(V3Property); }
            set
            {
                SetValue(V3Property, value); RaiseItemsChanged();
            }
        }

        // Using a DependencyProperty as the backing store for V3.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty V3Property =
            DependencyProperty.Register("V3", typeof(double), typeof(ValueBoxModel), new PropertyMetadata(0.0));




        public double V4
        {
            get { return (double)GetValue(V4Property); }
            set
            {
                SetValue(V4Property, value);
                RaiseItemsChanged();
            }
        }

        // Using a DependencyProperty as the backing store for V4.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty V4Property =
            DependencyProperty.Register("V4", typeof(double), typeof(ValueBoxModel), new PropertyMetadata(0.0));






        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ValueBoxModel), new PropertyMetadata(false));




        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(ValueBoxModel), new PropertyMetadata(""));





        public Visibility BoxVisibility
        {
            get { return (Visibility)GetValue(BoxVisibilityProperty); }
            set { SetValue(BoxVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoxVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoxVisibilityProperty =
            DependencyProperty.Register("BoxVisibility", typeof(Visibility), typeof(ValueBoxModel), new PropertyMetadata(Visibility.Visible));







        private readonly CartesianItems _selectionitems = new CartesianItems();
        public CartesianItems SelectionItems { get { return _selectionitems; } }




        public CartesianEnum SelectedItem
        {
            get { return (CartesianEnum)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(CartesianEnum), typeof(ValueBoxModel), new PropertyMetadata(CartesianEnum.ABB_Quaternion));


        #endregion

       


        void CheckVisibility()
        {
            switch (SelectedItem)
            {
                case CartesianEnum.ABB_Quaternion:
                case CartesianEnum.Axis_Angle:
                    BoxVisibility = Visibility.Visible;
                    break;
                default:
                    BoxVisibility = Visibility.Collapsed;
                    break;
            }

        }
    }
}