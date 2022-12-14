using System;
using System.Windows;

namespace miRobotEditor.Core.Classes
{
    public sealed class Item : DependencyObject
    {

        public Item(string type, string description)
        {
            Type = type;
            Description = description;
        }

        
        #region Index
        /// <summary>
        /// The <see cref="Index" /> dependency property's name.
        /// </summary>
        private const string IndexPropertyName = "Index";

        /// <summary>
        /// Gets or sets the value of the <see cref="Index" />
        /// property. This is a dependency property.
        /// </summary>
        public int Index
        {
            get
            {
                return (int)GetValue(IndexProperty);
            }
            set
            {
                SetValue(IndexProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Index" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register(
            IndexPropertyName,
            typeof(int),
            typeof(Item),
            new UIPropertyMetadata(-1));
        #endregion

        
        #region Type
        /// <summary>
        /// The <see cref="Type" /> dependency property's name.
        /// </summary>
        private const string TypePropertyName = "Type";

        /// <summary>
        /// Gets or sets the value of the <see cref="Type" />
        /// property. This is a dependency property.
        /// </summary>
        private string Type
        {
            get
            {
                return (string)GetValue(TypeProperty);
            }
            set
            {
                SetValue(TypeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Type" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            TypePropertyName,
            typeof(string),
            typeof(Item),
            new UIPropertyMetadata(""));
        #endregion
        
       #region Description
       /// <summary>
        /// The <see cref="Description" /> dependency property's name.
        /// </summary>
       private const string DescriptionPropertyName = "Description";

        /// <summary>
        /// Gets or sets the value of the <see cref="Description" />
        /// property. This is a dependency property.
        /// </summary>
        private string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Description" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            DescriptionPropertyName, 
            typeof(string), 
            typeof(Item), 
            new UIPropertyMetadata(0));
            #endregion


        public override string ToString()
        {
            return String.Format("{0};{1}", Type, Description);
        }
    }
}
