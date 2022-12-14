using System.Windows;

namespace miRobotEditor.Core
{
    public sealed class ToolTipModel:DependencyObject
    {

        private static ToolTipModel _instance;
        public static ToolTipModel Instance{ get 
            {
                return _instance ?? new ToolTipModel();
            } set
            {
                _instance = value;
            }}

        
        #region Message
        /// <summary>
        /// The <see cref="Message" /> dependency property's name.
        /// </summary>
        private const string MessagePropertyName = "Message";

        /// <summary>
        /// Gets or sets the value of the <see cref="Message" />
        /// property. This is a dependency property.
        /// </summary>
        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Message" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            MessagePropertyName,
            typeof(string),
            typeof(ToolTipModel),
            new UIPropertyMetadata(""));
        #endregion

        
        #region Title
        /// <summary>
        /// The <see cref="Title" /> dependency property's name.
        /// </summary>
        private const string TitlePropertyName = "Title";

        /// <summary>
        /// Gets or sets the value of the <see cref="Title" />
        /// property. This is a dependency property.
        /// </summary>
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Title" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            TitlePropertyName,
            typeof(string),
            typeof(ToolTipModel),
            new UIPropertyMetadata(""));
        #endregion
    }
}
