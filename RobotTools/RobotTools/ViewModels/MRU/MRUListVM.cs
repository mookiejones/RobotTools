using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace RobotTools.ViewModels.MRU
{
    public class MRUListVM : Base.BaseViewModel
    {
        #region Fields
        private MRUSortMethod mPinEntryAtHeadOfList = MRUSortMethod.PinnedEntriesFirst;

        private ObservableCollection<MRUEntryVM> mListOfMRUEntries;

        private int mMaxMruEntryCount;

        private RelayCommand _removeLastEntryCommand;
        private RelayCommand _removeFirstEntryCommand;
        #endregion Fields

        #region Constructor
        public MRUListVM()
        {
            mMaxMruEntryCount = 15;
            mPinEntryAtHeadOfList = MRUSortMethod.PinnedEntriesFirst;
        }

        public MRUListVM(MRUSortMethod pinEntryAtHeadOfList = MRUSortMethod.PinnedEntriesFirst)
          : this()
        {
            mPinEntryAtHeadOfList = pinEntryAtHeadOfList;
        }
        #endregion Constructor

        #region Properties
        [XmlAttribute(AttributeName = "MinValidMRUCount")]
        public int MinValidMruEntryCount
        {
            get
            {
                return 5;
            }
        }

        [XmlAttribute(AttributeName = "MaxValidMRUCount")]
        public int MaxValidMruEntryCount
        {
            get
            {
                return 256;
            }
        }

        [XmlAttribute(AttributeName = "MaxMruEntryCount")]
        public int MaxMruEntryCount
        {
            get
            {
                return mMaxMruEntryCount;
            }

            set
            {
                if (mMaxMruEntryCount != value)
                {
                    if (value < MinValidMruEntryCount || value > MaxValidMruEntryCount)
                        throw new ArgumentOutOfRangeException("MaxMruEntryCount", value, "Valid values are: value >= 5 and value <= 256");

                    mMaxMruEntryCount = value;

                    NotifyPropertyChanged(() => MaxMruEntryCount);
                }
            }
        }

        /// <summary>
        /// Get/set property to determine whether a pinned entry is shown
        /// 1> at the beginning of the MRU list
        /// or
        /// 2> remains where it currently is.
        /// </summary>
        [XmlAttribute(AttributeName = "SortMethod")]
        public MRUSortMethod PinSortMode
        {
            get
            {
                return mPinEntryAtHeadOfList;
            }

            set
            {
                if (mPinEntryAtHeadOfList != value)
                {
                    mPinEntryAtHeadOfList = value;
                    NotifyPropertyChanged(() => PinSortMode);
                }
            }
        }

        [XmlArrayItem("MRUList", IsNullable = false)]
        public ObservableCollection<MRUEntryVM> ListOfMRUEntries
        {
            get
            {
                return mListOfMRUEntries;
            }

            set
            {
                if (mListOfMRUEntries != value)
                {
                    mListOfMRUEntries = value;

                    NotifyPropertyChanged(() => ListOfMRUEntries);
                }
            }
        }

        #region RemoveEntryCommands
        public ICommand RemoveFirstEntryCommand
        {
            get
            {
                if (_removeFirstEntryCommand == null)
                    _removeFirstEntryCommand =
                        new RelayCommand(() => OnRemoveMRUEntry(MRUList.Spot.First));

                return _removeFirstEntryCommand;
            }
        }

        public ICommand RemoveLastEntryCommand
        {
            get
            {
                if (_removeLastEntryCommand == null)
                    _removeLastEntryCommand = new RelayCommand(() => OnRemoveMRUEntry(MRUList.Spot.Last));

                return _removeLastEntryCommand;
            }
        }

        #endregion RemoveEntryCommands
        #endregion Properties

        #region Methods
        #region AddRemove Methods
        private void OnRemoveMRUEntry(MRUList.Spot addInSpot = MRUList.Spot.Last)
        {
            if (mListOfMRUEntries == null)
                return;

            if (mListOfMRUEntries.Count == 0)
                return;

            switch (addInSpot)
            {
                case MRUList.Spot.Last:
                    mListOfMRUEntries.RemoveAt(mListOfMRUEntries.Count - 1);
                    break;
                case MRUList.Spot.First:
                    mListOfMRUEntries.RemoveAt(0);
                    break;

                default:
                    break;
            }

            //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);
        }

        private int CountPinnedEntries()
        {
            if (mListOfMRUEntries != null)
                return mListOfMRUEntries.Count(mru => mru.IsPinned == true);

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bPinOrUnPinMruEntry"></param>
        /// <param name="mruEntry"></param>
        public bool PinUnpinEntry(bool bPinOrUnPinMruEntry, MRUEntryVM mruEntry)
        {
            try
            {
                if (mListOfMRUEntries == null)
                    return false;

                int PinnedMruEntryCount = CountPinnedEntries();

                // pin an MRU entry into the next available pinned mode spot
                if (bPinOrUnPinMruEntry == true)
                {
                    MRUEntryVM e = mListOfMRUEntries.Single(mru => mru.IsPinned == false && mru.PathFileName == mruEntry.PathFileName);

                    if (PinSortMode == MRUSortMethod.PinnedEntriesFirst)
                        mListOfMRUEntries.Remove(e);

                    e.IsPinned = true;

                    if (PinSortMode == MRUSortMethod.PinnedEntriesFirst)
                        mListOfMRUEntries.Insert(PinnedMruEntryCount, e);

                    PinnedMruEntryCount += 1;
                    //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

                    return true;
                }
                else
                {
                    // unpin an MRU entry into the next available unpinned spot
                    MRUEntryVM e = mListOfMRUEntries.Single(mru => mru.IsPinned == true && mru.PathFileName == mruEntry.PathFileName);

                    if (PinSortMode == MRUSortMethod.PinnedEntriesFirst)
                        mListOfMRUEntries.Remove(e);

                    e.IsPinned = false;
                    PinnedMruEntryCount -= 1;

                    if (PinSortMode == MRUSortMethod.PinnedEntriesFirst)
                        mListOfMRUEntries.Insert(PinnedMruEntryCount, e);

                    //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

                    return true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(AppName + " encountered an error when pinning an entry:" + Environment.NewLine
                              + Environment.NewLine
                              + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }

        /// <summary>
        /// Standard short-cut method to add a new unpinned entry from a string
        /// </summary>
        /// <param name="newEntry">File name and path file</param>
        public void AddMRUEntry(string newEntry)
        {
            if (newEntry == null || newEntry == string.Empty)
                return;

            AddMRUEntry(new MRUEntryVM() { IsPinned = false, PathFileName = newEntry });
        }

        public void AddMRUEntry(MRUEntryVM newEntry)
        {
            if (newEntry == null) return;

            try
            {
                if (mListOfMRUEntries == null)
                    mListOfMRUEntries = new ObservableCollection<MRUEntryVM>();

                // Remove all entries that point to the path we are about to insert
                MRUEntryVM e = mListOfMRUEntries.SingleOrDefault(item => newEntry.PathFileName == item.PathFileName);

                if (e != null)
                {
                    // Do not change an entry that has already been pinned -> its pinned in place :)
                    if (e.IsPinned == true)
                        return;

                    mListOfMRUEntries.Remove(e);
                }

                // Remove last entry if list has grown too long
                if (MaxMruEntryCount <= mListOfMRUEntries.Count)
                    mListOfMRUEntries.RemoveAt(mListOfMRUEntries.Count - 1);

                // Add model entry in ViewModel collection (First pinned entry or first unpinned entry)
                if (newEntry.IsPinned == true)
                    mListOfMRUEntries.Insert(0, new MRUEntryVM(newEntry));
                else
                {
                    mListOfMRUEntries.Insert(CountPinnedEntries(), new MRUEntryVM(newEntry));
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "An error has occurred", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ////finally
            ////{
            ////   this.NotifyPropertyChanged(() => this.ListOfMRUEntries);
            ////}
        }

        public bool RemoveEntry(string fileName)
        {
            try
            {
                if (mListOfMRUEntries == null)
                    return false;

                MRUEntryVM e = mListOfMRUEntries.Single(mru => mru.PathFileName == fileName);

                mListOfMRUEntries.Remove(e);

                //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(AppName + " encountered an error when removing an entry:" + Environment.NewLine
                              + Environment.NewLine
                              + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }

        public bool RemovePinEntry(MRUEntryVM mruEntry)
        {
            try
            {
                if (mListOfMRUEntries == null)
                    return false;

                MRUEntryVM e = mListOfMRUEntries.Single(mru => mru.PathFileName == mruEntry.PathFileName);

                mListOfMRUEntries.Remove(e);

                //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(AppName + " encountered an error when removing an entry:" + Environment.NewLine
                              + Environment.NewLine
                              + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }
        #endregion AddRemove Methods

        public MRUEntryVM FindMRUEntry(string filePathName)
        {
            try
            {
                if (mListOfMRUEntries == null)
                    return null;

                return mListOfMRUEntries.SingleOrDefault(mru => mru.PathFileName == filePathName);
            }
            catch (Exception exp)
            {
                MessageBox.Show(AppName + " encountered an error when removing an entry:" + Environment.NewLine
                              + Environment.NewLine
                              + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }

        private string AppName
        {
            get
            {
                return Application.ResourceAssembly.GetName().Name;
            }
        }
        #endregion Methods
    }
}
