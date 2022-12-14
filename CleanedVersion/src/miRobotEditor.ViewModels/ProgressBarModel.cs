using System.Windows;

namespace miRobotEditor.ViewModels
{
    public class ProgressBarModel:DependencyObject
    {

        
        #region Maximum
        /// <summary>
        /// The <see cref="Maximum" /> dependency property's name.
        /// </summary>
        private const string MaximumPropertyName = "Maximum";

        /// <summary>
        /// Gets or sets the value of the <see cref="Maximum" />
        /// property. This is a dependency property.
        /// </summary>
        public int Maximum
        {
            get
            {
                return (int)GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Maximum" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            MaximumPropertyName,
            typeof(int),
            typeof(ProgressBarModel),
            new UIPropertyMetadata(100));
        #endregion

        
        #region Minimum
        /// <summary>
        /// The <see cref="Minimum" /> dependency property's name.
        /// </summary>
        private const string MinimumPropertyName = "Minimum";

        /// <summary>
        /// Gets or sets the value of the <see cref="Minimum" />
        /// property. This is a dependency property.
        /// </summary>
        public int Minimum
        {
            get
            {
                return (int)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Minimum" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            MinimumPropertyName,
            typeof(int),
            typeof(ProgressBarModel),
            new UIPropertyMetadata(0));
        #endregion

        
        #region Value
        /// <summary>
        /// The <see cref="Value" /> dependency property's name.
        /// </summary>
        private const string ValuePropertyName = "Value";

        /// <summary>
        /// Gets or sets the value of the <see cref="Value" />
        /// property. This is a dependency property.
        /// </summary>
        public int Value
        {
            get
            {
                return (int)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Value" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            ValuePropertyName,
            typeof(int),
            typeof(ProgressBarModel),
            new UIPropertyMetadata(0));
        #endregion

        
        #region IsVisibile
        /// <summary>
        /// The <see cref="IsVisible" /> dependency property's name.
        /// </summary>
        private const string IsVisibilePropertyName = "IsVisible";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsVisible" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return (bool)GetValue(IsVisibleProperty);
            }
            set
            {
                SetValue(IsVisibleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsVisible" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(
            IsVisibilePropertyName,
            typeof(bool),
            typeof(ProgressBarModel),
            new UIPropertyMetadata(false));
        #endregion


    }
}
