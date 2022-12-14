using System.Collections.ObjectModel;
using System.Windows;

namespace miRobotEditor.Core
{
    public sealed class InfoFile:DependencyObject
    {

        
        #region ArchiveName
        /// <summary>
        /// The <see cref="ArchiveName" /> dependency property's name.
        /// </summary>
        private const string ArchiveNamePropertyName = "ArchiveName";

        /// <summary>
        /// Gets or sets the value of the <see cref="ArchiveName" />
        /// property. This is a dependency property.
        /// </summary>
        public string ArchiveName
        {
            get
            {
                return (string)GetValue(ArchiveNameProperty);
            }
            set
            {
                SetValue(ArchiveNameProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ArchiveName" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchiveNameProperty = DependencyProperty.Register(
            ArchiveNamePropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion

        
        #region ArchiveConfigType
        /// <summary>
        /// The <see cref="ArchiveConfigType" /> dependency property's name.
        /// </summary>
        private const string ArchiveConfigTypePropertyName = "ArchiveConfigType";

        /// <summary>
        /// Gets or sets the value of the <see cref="ArchiveConfigType" />
        /// property. This is a dependency property.
        /// </summary>
        public string ArchiveConfigType
        {
            get
            {
                return (string)GetValue(ArchiveConfigTypeProperty);
            }
            set
            {
                SetValue(ArchiveConfigTypeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ArchiveConfigType" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchiveConfigTypeProperty = DependencyProperty.Register(
            ArchiveConfigTypePropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion

        
        #region ArchiveDiskNo
        /// <summary>
        /// The <see cref="ArchiveDiskNo" /> dependency property's name.
        /// </summary>
        private const string ArchiveDiskNoPropertyName = "ArchiveDiskNo";

        /// <summary>
        /// Gets or sets the value of the <see cref="ArchiveDiskNo" />
        /// property. This is a dependency property.
        /// </summary>
        public string ArchiveDiskNo
        {
            get
            {
                return (string)GetValue(ArchiveDiskNoProperty);
            }
            set
            {
                SetValue(ArchiveDiskNoProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ArchiveDiskNo" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchiveDiskNoProperty = DependencyProperty.Register(
            ArchiveDiskNoPropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion
        
        #region ArchiveID
        /// <summary>
        /// The <see cref="ArchiveID" /> dependency property's name.
        /// </summary>
        private const string ArchiveIDPropertyName = "ArchiveID";

        /// <summary>
        /// Gets or sets the value of the <see cref="ArchiveID" />
        /// property. This is a dependency property.
        /// </summary>
        public string ArchiveID
        {
            get
            {
                return (string)GetValue(ArchiveIDProperty);
            }
            set
            {
                SetValue(ArchiveIDProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ArchiveID" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchiveIDProperty = DependencyProperty.Register(
            ArchiveIDPropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion        
        
        
        #region ArchiveDate
        /// <summary>
        /// The <see cref="ArchiveDate" /> dependency property's name.
        /// </summary>
        private const string ArchiveDatePropertyName = "ArchiveDate";

        /// <summary>
        /// Gets or sets the value of the <see cref="ArchiveDate" />
        /// property. This is a dependency property.
        /// </summary>
        public string ArchiveDate
        {
            get
            {
                return (string)GetValue(ArchiveDateProperty);
            }
            set
            {
                SetValue(ArchiveDateProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ArchiveDate" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchiveDateProperty = DependencyProperty.Register(
            ArchiveDatePropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion

        
        #region RobotName
        /// <summary>
        /// The <see cref="RobotName" /> dependency property's name.
        /// </summary>
        private const string RobotNamePropertyName = "RobotName";

        /// <summary>
        /// Gets or sets the value of the <see cref="RobotName" />
        /// property. This is a dependency property.
        /// </summary>
        public string RobotName
        {
            get
            {
                return (string)GetValue(RobotNameProperty);
            }
            set
            {
                SetValue(RobotNameProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="RobotName" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty RobotNameProperty = DependencyProperty.Register(
            RobotNamePropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion     
        #region RobotSerial
        /// <summary>
        /// The <see cref="RobotSerial" /> dependency property's name.
        /// </summary>
        private const string RobotSerialPropertyName = "RobotSerial";

        /// <summary>
        /// Gets or sets the value of the <see cref="RobotSerial" />
        /// property. This is a dependency property.
        /// </summary>
        public string RobotSerial
        {
            get
            {
                return (string)GetValue(RobotSerialProperty);
            }
            set
            {
                SetValue(RobotSerialProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="RobotSerial" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty RobotSerialProperty = DependencyProperty.Register(
            RobotSerialPropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion

        
        #region KSSVersion
        /// <summary>
        /// The <see cref="KSSVersion" /> dependency property's name.
        /// </summary>
        private const string ArchiveKSSVersionPropertyName = "ArchiveKSSVersion";

        /// <summary>
        /// Gets or sets the value of the <see cref="KSSVersion" />
        /// property. This is a dependency property.
        /// </summary>
        public string KSSVersion
        {
            get
            {
                return (string)GetValue(ArchiveKSSVersionProperty);
            }
            set
            {
                SetValue(ArchiveKSSVersionProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="KSSVersion" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchiveKSSVersionProperty = DependencyProperty.Register(
            ArchiveKSSVersionPropertyName,
            typeof(string),
            typeof(InfoFile),
            new UIPropertyMetadata(""));
        #endregion

      

       

        private readonly ObservableCollection<Technology> _technologies = new ObservableCollection<Technology>();
        readonly ReadOnlyObservableCollection<Technology> _readonlyTechnology = null;
        public ReadOnlyObservableCollection<Technology> Technologies { get { return _readonlyTechnology ?? new ReadOnlyObservableCollection<Technology>(_technologies); } }

    }
}