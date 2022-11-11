using System.Xml.Serialization;

namespace RobotTools.ViewModels.MRU
{
    public class MRUEntryVM : Base.BaseViewModel
    {
        #region fields
        private MRUEntry mMRUEntry;
        #endregion fields

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MRUEntryVM()
        {
            mMRUEntry = new MRUEntry();
            IsPinned = false;
        }

        /// <summary>
        /// Constructor from model
        /// </summary>
        /// <param name="model"></param>
        public MRUEntryVM(MRUEntry model) : this()
        {
            mMRUEntry = new MRUEntry(model);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copySource"></param>
        public MRUEntryVM(MRUEntryVM copySource)
          : this()
        {
            mMRUEntry = new MRUEntry(copySource.mMRUEntry);
            IsPinned = copySource.IsPinned;
        }
        #endregion Constructor

        #region Properties
        [XmlAttribute(AttributeName = "PathFileName")]
        public string PathFileName
        {
            get
            {
                return mMRUEntry.PathFileName;
            }

            set
            {
                if (mMRUEntry.PathFileName != value)
                {
                    mMRUEntry.PathFileName = value;
                    NotifyPropertyChanged(() => PathFileName);
                    NotifyPropertyChanged(() => DisplayPathFileName);
                }
            }
        }

        [XmlIgnore]
        public string DisplayPathFileName
        {
            get
            {
                if (mMRUEntry == null)
                    return string.Empty;

                if (mMRUEntry.PathFileName == null)
                    return string.Empty;

                int n = 32;
                return (mMRUEntry.PathFileName.Length > n ? mMRUEntry.PathFileName.Substring(0, 3) +
                                                        "... " + mMRUEntry.PathFileName.Substring(mMRUEntry.PathFileName.Length - n)
                                                      : mMRUEntry.PathFileName);
            }
        }

        [XmlAttribute(AttributeName = "IsPinned")]
        public bool IsPinned
        {
            get
            {
                return mMRUEntry.IsPinned;
            }

            set
            {
                if (mMRUEntry.IsPinned != value)
                {
                    mMRUEntry.IsPinned = value;
                    NotifyPropertyChanged(() => IsPinned);
                }
            }
        }
        #endregion Properties
    }
}
