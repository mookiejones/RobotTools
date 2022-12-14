using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Ionic.Zip;
using Microsoft.Win32;
using miRobotEditor.Core;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core.Classes.Messaging;

namespace miRobotEditor.ViewModels
{
    public class IOViewModel : ViewModelBase
    {
        #region Properties

        private readonly List<Item> _anin = new List<Item>();
        private readonly List<Item> _anout = new List<Item>();
        private readonly List<Item> _counter = new List<Item>();
        private readonly List<Item> _cycflags = new List<Item>();
        private readonly List<Item> _flags = new List<Item>();
        private readonly List<Item> _inputs = new List<Item>();
        private readonly List<Item> _outputs = new List<Item>();
        private readonly ReadOnlyCollection<Item> _readonlyAnIn = null;
        private readonly ReadOnlyCollection<Item> _readonlyAnOut = null;
        private readonly ReadOnlyCollection<Item> _readonlyCounter = null;
        private readonly ReadOnlyCollection<Item> _readonlyCycFlags = null;
        private readonly ReadOnlyCollection<Item> _readonlyFlags = null;
        private readonly ReadOnlyCollection<Item> _readonlyOutputs = null;
        private readonly ReadOnlyObservableCollection<DirectoryInfo> _readonlyRoot = null;
        private readonly ReadOnlyCollection<Item> _readonlyTimer = null;
        private readonly ReadOnlyCollection<Item> _readonlyinputs = null;
        private readonly ObservableCollection<DirectoryInfo> _root = new ObservableCollection<DirectoryInfo>();
        private readonly List<Item> _timer = new List<Item>();
      
        public Visibility DigInVisibility
        {
            get { return Inputs.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
        }

        public Visibility DigOutVisibility
        {
            get { return Outputs.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
        }

        public Visibility AnInVisibility
        {
            get { return AnIn.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
        }

        public Visibility AnOutVisibility
        {
            get { return AnOut.Count > 0 ? Visibility.Visible : Visibility.Hidden; }
        }


        public Visibility DigitalVisibility
        {
            get
            {
                return ((DigInVisibility == Visibility.Visible) && (DigOutVisibility == Visibility.Visible))
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility AnalogVisibility
        {
            get
            {
                return ((AnOutVisibility == Visibility.Visible) || (AnInVisibility == Visibility.Visible))
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        
        #region FlagVisibility
        /// <summary>
        /// The <see cref="FlagVisibility" /> property's name.
        /// </summary>
        public const string FlagVisibilityPropertyName = "FlagVisibility";

        private Visibility _flagVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the FlagVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility FlagVisibility
        {
            get
            {
                return _flagVisibility;
            }

            set
            {
                if (_flagVisibility == value)
                {
                    return;
                }

                RaisePropertyChanging(FlagVisibilityPropertyName);
                _flagVisibility = value;
                RaisePropertyChanged(FlagVisibilityPropertyName);
            }
        }
        #endregion

        
        #region TimerVisibility
        /// <summary>
        /// The <see cref="TimerVisibility" /> property's name.
        /// </summary>
        public const string TimerVisibilityPropertyName = "TimerVisibility";

        private Visibility _timerVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the TimerVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility TimerVisibility
        {
            get
            {
                return _timerVisibility;
            }

            set
            {
                if (_timerVisibility == value)
                {
                    return;
                }

                RaisePropertyChanging(TimerVisibilityPropertyName);
                _timerVisibility = value;
                RaisePropertyChanged(TimerVisibilityPropertyName);
            }
        }
        #endregion

        
        #region CyclicFlagVisibility
        /// <summary>
        /// The <see cref="CyclicFlagVisibility" /> property's name.
        /// </summary>
        public const string CyclicFlagVisibilityPropertyName = "CyclicFlagVisibility";

        private Visibility _cyclicFlagVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the CyclicFlagVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility CyclicFlagVisibility
        {
            get
            {
                return _cyclicFlagVisibility;
            }

            set
            {
                if (_cyclicFlagVisibility == value)
                {
                    return;
                }

                RaisePropertyChanging(CyclicFlagVisibilityPropertyName);
                _cyclicFlagVisibility = value;
                RaisePropertyChanged(CyclicFlagVisibilityPropertyName);
            }
        }
        #endregion

        
        #region CounterVisibility
        /// <summary>
        /// The <see cref="CounterVisibility" /> property's name.
        /// </summary>
        public const string CounterVisibilityPropertyName = "CounterVisibility";

        private Visibility _counterVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the CounterVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility CounterVisibility
        {
            get
            {
                return _counterVisibility;
            }

            set
            {
                if (_counterVisibility == value)
                {
                    return;
                }

                RaisePropertyChanging(CounterVisibilityPropertyName);
                _counterVisibility = value;
                RaisePropertyChanged(CounterVisibilityPropertyName);
            }
        }
        #endregion

        
        #region Info
        /// <summary>
        /// The <see cref="Info" /> property's name.
        /// </summary>
        public const string InfoPropertyName = "Info";

        private InfoFile _info = new InfoFile();

        /// <summary>
        /// Sets and gets the Info property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public InfoFile Info
        {
            get
            {
                return _info;
            }

            set
            {
                if (_info == value)
                {
                    return;
                }

                RaisePropertyChanging(InfoPropertyName);
                _info = value;
                RaisePropertyChanged(InfoPropertyName);
            }
        }
        #endregion

        
        #region ArchivePath
        /// <summary>
        /// The <see cref="ArchivePath" /> property's name.
        /// </summary>
        public const string ArchivePathPropertyName = "ArchivePath";

        private string _archivePath = String.Empty;

        /// <summary>
        /// Sets and gets the ArchivePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ArchivePath
        {
            get
            {
                return _archivePath;
            }

            set
            {
                if (_archivePath == value)
                {
                    return;
                }

                RaisePropertyChanging(ArchivePathPropertyName);
                _archivePath = value;
                RaisePropertyChanged(ArchivePathPropertyName);
            }
        }
        #endregion


        public string DirectoryPath { get; set; }




     

        
        #region FileCount
        /// <summary>
        /// The <see cref="FileCount" /> property's name.
        /// </summary>
        private const string FileCountPropertyName = "FileCount";

        private string _fileCount = String.Empty;

        /// <summary>
        /// Sets and gets the FileCount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FileCount
        {
            get
            {
                return _fileCount;
            }

            set
            {
                if (_fileCount == value)
                {
                    return;
                }

                RaisePropertyChanging(FileCountPropertyName);
                _fileCount = value;
                RaisePropertyChanged(FileCountPropertyName);
            }
        }
        #endregion

        public ZipFile ArchiveZip { get; set; }

        
        #region BufferSize
        /// <summary>
        /// The <see cref="BufferSize" /> property's name.
        /// </summary>
        private const string BufferSizePropertyName = "BufferSize";

        private string _bufferSize = String.Empty;

        /// <summary>
        /// Sets and gets the BufferSize property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string BufferSize
        {
            get
            {
                return _bufferSize;
            }

            set
            {
                if (_bufferSize == value)
                {
                    return;
                }

                RaisePropertyChanging(BufferSizePropertyName);
                _bufferSize = value;
                RaisePropertyChanged(BufferSizePropertyName);
            }
        }
        #endregion
      

        public string DataBaseFile { get; set; }
        // ReSharper disable ConvertToConstant.Local
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        // ReSharper restore ConvertToConstant.Local


        
        #region DataBase
        /// <summary>
        /// The <see cref="DataBase" /> property's name.
        /// </summary>
        private const string DataBasePropertyName = "DataBase";

        private string _dataBase = String.Empty;

        /// <summary>
        /// Sets and gets the DataBase property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DataBase
        {
            get
            {
                return _dataBase;
            }

            set
            {
                if (_dataBase == value)
                {
                    return;
                }

                RaisePropertyChanging(DataBasePropertyName);
                _dataBase = value;
                RaisePropertyChanged(DataBasePropertyName);
            }
        }
        #endregion
      

        public string InfoFile { get; set; }


        public ReadOnlyObservableCollection<DirectoryInfo> Root
        {
            get { return _readonlyRoot ?? new ReadOnlyObservableCollection<DirectoryInfo>(_root); }
        }

     
        
        #region RootPath
        /// <summary>
        /// The <see cref="RootPath" /> property's name.
        /// </summary>
        private const string RootPathPropertyName = "RootPath";

        private DirectoryInfo _rootPath = null;

        /// <summary>
        /// Sets and gets the RootPath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DirectoryInfo RootPath
        {
            get
            {
                return _rootPath;
            }

            set
            {
                if (_rootPath == value)
                {
                    return;
                }

                RaisePropertyChanging(RootPathPropertyName);
                _rootPath = value;
                RaisePropertyChanged(RootPathPropertyName);
            }
        }
        #endregion
   
        #region LanguageText
        /// <summary>
        /// The <see cref="LanguageText" /> property's name.
        /// </summary>
        private const string LanguageTextPropertyName = "LanguageText";

        private string _languageText = String.Empty;

        /// <summary>
        /// Sets and gets the LanguageText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LanguageText
        {
            get
            {
                return _languageText;
            }

            set
            {
                if (_languageText== value)
                {
                    return;
                }

                RaisePropertyChanging(LanguageTextPropertyName);
                _languageText=value;
                RaisePropertyChanged(LanguageTextPropertyName);
            }
        }
        #endregion

        
        #region DatabaseText
        /// <summary>
        /// The <see cref="DatabaseText" /> property's name.
        /// </summary>
        private const string DatabaseTextPropertyName = "DatabaseText";

        private string _databaseText = String.Empty;

        /// <summary>
        /// Sets and gets the DatabaseText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DatabaseText
        {
            get
            {
                return _databaseText;
            }

            set
            {
                if (_databaseText == value)
                {
                    return;
                }

                RaisePropertyChanging(DatabaseTextPropertyName);
                _databaseText = value;
                RaisePropertyChanged(DatabaseTextPropertyName);
            }
        }
        #endregion

     


        public ReadOnlyCollection<Item> Inputs
        {
            get { return _readonlyinputs ?? new ReadOnlyCollection<Item>(_inputs); }
        }

        public ReadOnlyCollection<Item> Outputs
        {
            get { return _readonlyOutputs ?? new ReadOnlyCollection<Item>(_outputs); }
        }

        public ReadOnlyCollection<Item> AnIn
        {
            get { return _readonlyAnIn ?? new ReadOnlyCollection<Item>(_anin); }
        }

        public ReadOnlyCollection<Item> AnOut
        {
            get { return _readonlyAnOut ?? new ReadOnlyCollection<Item>(_anout); }
        }

        public ReadOnlyCollection<Item> Timer
        {
            get { return _readonlyTimer ?? new ReadOnlyCollection<Item>(_timer); }
        }

        public ReadOnlyCollection<Item> Flags
        {
            get { return _readonlyFlags ?? new ReadOnlyCollection<Item>(_flags); }
        }

        public ReadOnlyCollection<Item> CycFlags
        {
            get { return _readonlyCycFlags ?? new ReadOnlyCollection<Item>(_cycflags); }
        }

        public ReadOnlyCollection<Item> Counter
        {
            get { return _readonlyCounter ?? new ReadOnlyCollection<Item>(_counter); }
        }

        #endregion

        //TODO Make sure this is the right way to do things
        private static OleDbConnection oleDbConnection;
        private readonly BackgroundWorker _backgroundWorker;

        #region Constructor

        public IOViewModel(string filename)
        {
            DataBaseFile = filename;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.RunWorkerAsync();
        }

        #endregion

        #region Background Worker

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GetSignals();
            GetTimers();
            GetAllLangtextFromDatabase();
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RaisePropertyChanged("Inputs");
            RaisePropertyChanged("Outputs");
            RaisePropertyChanged("AnIn");
            RaisePropertyChanged("AnOut");
            RaisePropertyChanged("Counter");
            RaisePropertyChanged("Flags");
            RaisePropertyChanged("Timer");
        }

        #endregion

        public OleDbConnection GetDBConnection()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";


            // Test Connection
            try
            {
                if (oleDbConnection == null)
                    oleDbConnection = new OleDbConnection(connectionString);
                oleDbConnection.Open();
                oleDbConnection.Close();
            }
            catch (Exception ex)
            {
                var msg = new ErrorMessage("GetDBConnection", ex);
                Messenger.Default.Send(msg);
                oleDbConnection = null;
            }
            return oleDbConnection;
        }

        /// <summary>
        ///     Gets Signals from kuka_con.mdb
        /// </summary>
        private void GetSignals()
        {
            if (File.Exists(DataBaseFile))
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
                const string cmdText =
                    "SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = 'IO')";

                OleDbConnection oleDbConnection = GetDBConnection();
                if (oleDbConnection == null)
                    return;

                oleDbConnection.Open();

                using (var oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
                {
                    using (OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader())
                    {
                        while (oleDbDataReader != null && oleDbDataReader.Read())
                        {
                            string temp = oleDbDataReader.GetValue(0).ToString();
                            Item sig;
                            string des = oleDbDataReader.GetValue(1).ToString();
                            string description = des == "|EMPTY|" ? "Spare" : des;
                            switch (temp.Substring(0, temp.IndexOf("_")))
                            {
                                case "IN":
                                    sig = new Item(String.Format("$IN[{0}]", temp.Substring(3)), description);
                                    _inputs.Add(sig);
                                    LanguageText += sig + "\r\n";
                                    break;
                                case "OUT":
                                    sig = new Item(String.Format("$OUT[{0}]", temp.Substring(4)), description);
                                    if (!_outputs.Contains(sig))
                                        _outputs.Add(sig);
                                    LanguageText += sig + "\r\n";
                                    break;
                                case "ANIN":
                                    sig = new Item(String.Format("$ANIN[{0}]", temp.Substring(5)), description);
                                    _anin.Add(sig);
                                    LanguageText += sig + "\r\n";
                                    break;
                                case "ANOUT":
                                    sig = new Item(String.Format("$ANOUT[{0}]", temp.Substring(6)), description);
                                    _anout.Add(sig);
                                    LanguageText += sig + "\r\n";
                                    break;
                            }
                        }
                    }
                }
            }

            GetFlags();
            GetTimers();
            GetAllLangtextFromDatabase();
            RaisePropertyChanged("DigInVisibility");
            RaisePropertyChanged("DigOutVisibility");
            RaisePropertyChanged("AnalogVisibility");
            RaisePropertyChanged("DigitalVisibility");
        }

        /// <summary>
        ///     Gets Flags from kuka_con.mdb
        /// </summary>
        private void GetFlags()
        {
            if (!File.Exists(DataBaseFile)) return;
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
            const string cmdText =
                "SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = 'FLAG')";

            OleDbConnection oleDbConnection = GetDBConnection();
            if (oleDbConnection == null)
                return;
            oleDbConnection.Open();


            using (var oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
            {
                using (OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader())
                {
                    while (oleDbDataReader != null && oleDbDataReader.Read())
                    {
                        string temp = oleDbDataReader.GetValue(0).ToString();
                        var sig = new Item(String.Format("$FLAG[{0}]", temp.Substring(8)),
                            oleDbDataReader.GetValue(1).ToString());
                        _flags.Add(sig);
                    }
                }
            }
            FlagVisibility = Flags.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            RaisePropertyChanged("FlagVisibility");
        }

        /// <summary>
        ///     Gets Timers from kuka_con.mdb
        /// </summary>
        private void GetTimers()
        {
            const string cmdText =
                "SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = 'TIMER')";
            OleDbConnection oleDbConnection = GetDBConnection();
            if (oleDbConnection == null)
                return;
            oleDbConnection.Open();
            using (var oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
            {
                using (OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader())
                {
                    while (oleDbDataReader != null && oleDbDataReader.Read())
                    {
                        string temp = oleDbDataReader.GetValue(0).ToString();
                        var sig = new Item(String.Format("$TIMER[{0}]", temp.Substring(9)),
                            oleDbDataReader.GetValue(1).ToString());
                        _timer.Add(sig);
                    }
                }
            }
            TimerVisibility = Timer.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            RaisePropertyChanged("TimerVisibility");
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        // ReSharper disable UnusedMember.Local
        private List<Item> GetValues(string cmd, int index)
            // ReSharper restore UnusedMember.Local
        {
            var result = new List<Item>();
            string cmdText =
                String.Format(
                    "SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = '{0}')",
                    cmd);
            OleDbConnection oleDbConnection = GetDBConnection();
            if (oleDbConnection == null)
                return result;


            oleDbConnection.Open();
            using (var oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
            {
                using (OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader())
                {
                    while (oleDbDataReader != null && oleDbDataReader.Read())
                    {
                        string temp = oleDbDataReader.GetValue(0).ToString();
                        var sig = new Item(String.Format("${1}[{0}]", temp.Substring(index), cmd),
                            oleDbDataReader.GetValue(1).ToString());
                        result.Add(sig);
                    }
                }
            }

            return result;
        }

        // ReSharper disable UnusedMember.Local
        private void GetSignalsFromDataBase()
            // ReSharper restore UnusedMember.Local
        {
            var ofd = new OpenFileDialog
            {
                Title = "Select Database",
                Filter = "KUKA Connection Files (kuka_con.mdb)|kuka_con.mdb|All files (*.*)|*.*",
                Multiselect = false
            };
            ofd.ShowDialog();
            LanguageText = string.Empty;

            DataBaseFile = ofd.FileName;
            GetSignals();
        }

        private void GetAllLangtextFromDatabase()
        {
            LanguageText = string.Empty;

            if (!File.Exists(DataBaseFile)) return;
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
            const string cmdText =
                "SELECT i.keystring, m.string FROM ITEMS i, messages m where i.key_id=m.key_id and m.language_id=99";
            OleDbConnection oleDbConnection = GetDBConnection();
            if (oleDbConnection == null)
                return;
            oleDbConnection.Open();
            using (var oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
            {
                using (OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader())
                {
                    while (oleDbDataReader != null && oleDbDataReader.Read())
                    {
                        string str1 = oleDbDataReader.GetValue(0).ToString();
                        string str2 = oleDbDataReader.GetValue(1).ToString();
                        DataBase += string.Format("{0} {1}\r\n", str1, str2);
                    }
                }
            }
        }
    }
}