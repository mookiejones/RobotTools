using System.Windows;
using miRobotEditor.Core.Classes;

namespace miRobotEditor.UI.Windows
{
    /// <summary>
    /// Interaction logic for ShiftWindow.xaml
    /// </summary>
    public partial class ShiftWindow : Window
    {
        public ShiftWindow()
        {
            InitializeComponent();
        }



        
        #region OldValues
        /// <summary>
        /// The <see cref="OldValues" /> dependency property's name.
        /// </summary>
        public const string OldValuesPropertyName = "OldValues";

        /// <summary>
        /// Gets or sets the value of the <see cref="OldValues" />
        /// property. This is a dependency property.
        /// </summary>
        public CartesianPosition OldValues
        {
            get
            {
                return (CartesianPosition)GetValue(OldValuesProperty);
            }
            set
            {
                SetValue(OldValuesProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="OldValues" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty OldValuesProperty = DependencyProperty.Register(
            OldValuesPropertyName,
            typeof(CartesianPosition),
            typeof(ShiftWindow),
            new UIPropertyMetadata(new CartesianPosition()));
        #endregion


        
        #region NewValues
        /// <summary>
        /// The <see cref="NewValues" /> dependency property's name.
        /// </summary>
        public const string NewValuesPropertyName = "NewValues";

        /// <summary>
        /// Gets or sets the value of the <see cref="NewValues" />
        /// property. This is a dependency property.
        /// </summary>
        public CartesianPosition NewValues
        {
            get
            {
                return (CartesianPosition)GetValue(NewValuesProperty);
            }
            set
            {
                SetValue(NewValuesProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="NewValues" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty NewValuesProperty = DependencyProperty.Register(
            NewValuesPropertyName,
            typeof(CartesianPosition),
            typeof(ShiftWindow),
            new UIPropertyMetadata(new CartesianPosition()));
        #endregion
        
        #region DiffValues
        /// <summary>
        /// The <see cref="DiffValues" /> dependency property's name.
        /// </summary>
        public const string DiffValuesPropertyName = "DiffValues";

        /// <summary>
        /// Gets or sets the value of the <see cref="DiffValues" />
        /// property. This is a dependency property.
        /// </summary>
        public CartesianPosition DiffValues
        {
            get
            {
                return (CartesianPosition)GetValue(DiffValuesProperty);
            }
            set
            {
                SetValue(DiffValuesProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="DiffValues" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DiffValuesProperty = DependencyProperty.Register(
            DiffValuesPropertyName,
            typeof(CartesianPosition),
            typeof(ShiftWindow),
            new UIPropertyMetadata(new CartesianPosition()));
        #endregion   



    }
}
