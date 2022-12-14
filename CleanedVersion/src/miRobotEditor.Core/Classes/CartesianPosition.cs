using System.Windows;

namespace miRobotEditor.Core.Classes
{
    /// <summary>
    /// Cartesian Position to be used with the shift
    /// </summary>
    public sealed class CartesianPosition:DependencyObject
    {

        
        #region Header
        /// <summary>
        /// The <see cref="Header" /> dependency property's name.
        /// </summary>
        private const string HeaderPropertyName = "Header";

        /// <summary>
        /// Gets or sets the value of the <see cref="Header" />
        /// property. This is a dependency property.
        /// </summary>
        public string Header
        {
            get
            {
                return (string)GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Header" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            HeaderPropertyName,
            typeof(string),
            typeof(CartesianPosition),
            new UIPropertyMetadata(""));
        #endregion


        
        #region X
        /// <summary>
        /// The <see cref="X" /> dependency property's name.
        /// </summary>
        public const string XPropertyName = "X";

        /// <summary>
        /// Gets or sets the value of the <see cref="X" />
        /// property. This is a dependency property.
        /// </summary>
        public double X
        {
            get
            {
                return (double)GetValue(XProperty);
            }
            set
            {
                SetValue(XProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="X" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            XPropertyName,
            typeof(double),
            typeof(CartesianPosition),
            new UIPropertyMetadata(0));
        #endregion
        #region Y
        /// <summary>
        /// The <see cref="Y" /> dependency property's name.
        /// </summary>
        private const string YPropertyName = "Y";

        /// <summary>
        /// Gets or sets the value of the <see cref="Y" />
        /// property. This is a dependency property.
        /// </summary>
        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Y" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            YPropertyName,
            typeof(double),
            typeof(CartesianPosition),
            new UIPropertyMetadata(0));
        #endregion

        
        #region Z
        /// <summary>
        /// The <see cref="Z" /> dependency property's name.
        /// </summary>
        private const string ZPropertyName = "Z";

        /// <summary>
        /// Gets or sets the value of the <see cref="Z" />
        /// property. This is a dependency property.
        /// </summary>
        public double Z
        {
            get
            {
                return (double)GetValue(ZProperty);
            }
            set
            {
                SetValue(ZProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Z" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZProperty = DependencyProperty.Register(
            ZPropertyName,
            typeof(double),
            typeof(CartesianPosition),
            new UIPropertyMetadata(0));
        #endregion
    }
}