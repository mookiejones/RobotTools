using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using Microsoft.Win32;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core;
using Ionic.Zip;

namespace miRobotEditor.EditorControl
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
        private string _archivePath = " ";
        private string _buffersize = string.Empty;
        private Visibility _counterVisibility = Visibility.Collapsed;
        private Visibility _cyclicFlagVisibility = Visibility.Collapsed;
        private string _database = string.Empty;
        private string _databaseText = String.Empty;
        private string _filecount = string.Empty;
        private InfoFile _info = new InfoFile();
        private string _languageText = String.Empty;
        private DirectoryInfo _rootpath;
        private Visibility _timerVisibility = Visibility.Collapsed;

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
        private const string FlagVisibilityPropertyName = "FlagVisibility";

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
      


        public Visibility TimerVisibility
        {
            get { return _timerVisibility; }
            set
            {
                _timerVisibility = value;
                RaisePropertyChanged("TimerVisibility");
            }
        }

        public Visibility CyclicFlagVisibility
        {
            get { return _cyclicFlagVisibility; }
            set
            {
                _cyclicFlagVisibility = value;
                RaisePropertyChanged("CyclicFlagVisibility");
            }
        }

        public Visibility CounterVisibility
        {
            get { return _counterVisibility; }
            set
            {
                _counterVisibility = value;
                RaisePropertyChanged("CounterVisibility");
            }
        }

        public InfoFile Info
        {
            get { return _info; }
            set
            {
                _info = value;
                RaisePropertyChanged("Info");
            }
        }


        public string DirectoryPath { get; set; }

        public string ArchivePath
        {
            get { return _archivePath; }
            set
            {
                _archivePath = value;
                RaisePropertyChanged("ArchivePath");
            }
        }


        public string FileCount
        {
            get { return _filecount; }
            set
            {
                _filecount = value;
                RaisePropertyChanged("FileCount");
            }
        }

        public ZipFile ArchiveZip { get; set; }

        public string BufferSize
        {
            get { return _buffersize; }
            set
            {
                _buffersize = value;
                RaisePropertyChanged("BufferSize");
            }
        }

        public string DataBaseFile { get; set; }
        // ReSharper disable ConvertToConstant.Local
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        // ReSharper restore ConvertToConstant.Local

        public string DataBase
        {
            get { return _database; }
            set
            {
                _database = value;
                RaisePropertyChanged("DataBase");
            }
        }

        public string InfoFile { get; set; }


        public ReadOnlyObservableCollection<DirectoryInfo> Root
        {
            get { return _readonlyRoot ?? new ReadOnlyObservableCollection<DirectoryInfo>(_root); }
        }

        public DirectoryInfo RootPath
        {
            get { return _rootpath; }
            set
            {
                _rootpath = value;
                RaisePropertyChanged("RootPath");
            }
        }

        public string LanguageText
        {
            get { return _languageText; }
            set
            {
                _languageText = value;
                RaisePropertyChanged("LanguageText");
            }
        }

        public string DatabaseText
        {
            get { return _databaseText; }
            set
            {
                _databaseText = value;
                RaisePropertyChanged("DatabaseText");
            }
        }


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