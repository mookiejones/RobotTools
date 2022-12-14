using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Ionic.Zip;
using Microsoft.Win32;
using miRobotEditor.Core;
using miRobotEditor.Core.Classes;

namespace miRobotEditor.ViewModels
{
    public sealed class ArchiveInfoModel:DependencyObject
    {
        #region Constructor
        public ArchiveInfoModel()        {
            
            _root = new ObservableCollection<DirectoryInfo>();

          }
        #endregion

        #region Properties

        public Visibility DigInVisibility { get { return Inputs.Count > 0 ? Visibility.Visible : Visibility.Hidden; } }
        public Visibility DigOutVisibility { get { return Outputs.Count > 0 ? Visibility.Visible : Visibility.Hidden; } }
        public Visibility AnInVisibility { get { return AnIn.Count > 0 ? Visibility.Visible : Visibility.Hidden; } }
        public Visibility AnOutVisibility { get { return AnOut.Count > 0 ? Visibility.Visible : Visibility.Hidden; } }


        public Visibility DigitalVisibility
        {
            get
            {
                return ((DigInVisibility == Visibility.Visible) && (DigOutVisibility == Visibility.Visible))
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }


        
        #region AnalogVisibility
        /// <summary>
        /// The <see cref="AnalogVisibility" /> dependency property's name.
        /// </summary>
        private const string AnalogVisibilityPropertyName = "AnalogVisibility";

        /// <summary>
        /// Gets or sets the value of the <see cref="AnalogVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility AnalogVisibility
        {
            get
            {
                return (Visibility)GetValue(AnalogVisibilityProperty);
            }
            set
            {
                SetValue(AnalogVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="AnalogVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnalogVisibilityProperty = DependencyProperty.Register(
            AnalogVisibilityPropertyName,
            typeof(Visibility),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(Visibility.Collapsed));
        #endregion
        
        #region FlagVisibility
        /// <summary>
        /// The <see cref="FlagVisibility" /> dependency property's name.
        /// </summary>
        private const string FlagVisibilityPropertyName = "FlagVisibility";

        /// <summary>
        /// Gets or sets the value of the <see cref="FlagVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility FlagVisibility
        {
            get
            {
                return (Visibility)GetValue(FlagVisibilityProperty);
            }
            set
            {
                SetValue(FlagVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="FlagVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FlagVisibilityProperty = DependencyProperty.Register(
            FlagVisibilityPropertyName,
            typeof(Visibility),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(Visibility.Collapsed));
        #endregion
       
        
       #region TimerVisibility
       /// <summary>
        /// The <see cref="TimerVisibility" /> dependency property's name.
        /// </summary>
        private const string TimerVisibilityPropertyName = "TimerVisibility";

        /// <summary>
        /// Gets or sets the value of the <see cref="TimerVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility TimerVisibility
        {
            get
            {
                return (Visibility)GetValue(TimerVisibilityProperty);
            }
            set
            {
                SetValue(TimerVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="TimerVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TimerVisibilityProperty = DependencyProperty.Register(
            TimerVisibilityPropertyName, 
            typeof(Visibility), 
            typeof(ArchiveInfoModel), 
            new UIPropertyMetadata(Visibility.Collapsed));
            #endregion


        
       #region CyclicFlagVisibility
       /// <summary>
        /// The <see cref="CyclicFlagVisibility" /> dependency property's name.
        /// </summary>
        private const string CyclicFlagVisibilityPropertyName = "CyclicFlagVisibility";

        /// <summary>
        /// Gets or sets the value of the <see cref="CyclicFlagVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility CyclicFlagVisibility
        {
            get
            {
                return (Visibility)GetValue(CyclicFlagVisibilityProperty);
            }
            set
            {
                SetValue(CyclicFlagVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CyclicFlagVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CyclicFlagVisibilityProperty = DependencyProperty.Register(
            CyclicFlagVisibilityPropertyName, 
            typeof(Visibility), 
            typeof(ArchiveInfoModel), 
            new UIPropertyMetadata(Visibility.Collapsed));
            #endregion
    
        
       #region CounterVisibility
       /// <summary>
        /// The <see cref="CounterVisibility" /> dependency property's name.
        /// </summary>
        private const string CounterVisibilityPropertyName = "CounterVisibility";

        /// <summary>
        /// Gets or sets the value of the <see cref="CounterVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility CounterVisibility
        {
            get
            {
                return (Visibility)GetValue(CounterVisibilityProperty);
            }
            set
            {
                SetValue(CounterVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CounterVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CounterVisibilityProperty = DependencyProperty.Register(
            CounterVisibilityPropertyName, 
            typeof(Visibility), 
            typeof(ArchiveInfoModel), 
            new UIPropertyMetadata(Visibility.Collapsed));
            #endregion
       
        private readonly ProgressBarModel _progress = new ProgressBarModel();


        
        #region Info
        /// <summary>
        /// The <see cref="Info" /> dependency property's name.
        /// </summary>
        private const string InfoPropertyName = "Info";

        /// <summary>
        /// Gets or sets the value of the <see cref="Info" />
        /// property. This is a dependency property.
        /// </summary>
        public InfoFile Info
        {
            get
            {
                return (InfoFile)GetValue(InfoProperty);
            }
            set
            {
                SetValue(InfoProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Info" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty InfoProperty = DependencyProperty.Register(
            InfoPropertyName,
            typeof(InfoFile),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(new InfoFile()));
        #endregion

        
        #region DirectoryPath
        /// <summary>
        /// The <see cref="DirectoryPath" /> dependency property's name.
        /// </summary>
        private const string DirectoryPathPropertyName = "DirectoryPath";

        /// <summary>
        /// Gets or sets the value of the <see cref="DirectoryPath" />
        /// property. This is a dependency property.
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return (string)GetValue(DirectoryPathProperty);
            }
            set
            {
                SetValue(DirectoryPathProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="DirectoryPath" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DirectoryPathProperty = DependencyProperty.Register(
            DirectoryPathPropertyName,
            typeof(string),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(""));
        #endregion

        
        #region ArchivePath
        /// <summary>
        /// The <see cref="ArchivePath" /> dependency property's name.
        /// </summary>
        private const string ArchivePathPropertyName = "ArchivePath";

        /// <summary>
        /// Gets or sets the value of the <see cref="ArchivePath" />
        /// property. This is a dependency property.
        /// </summary>
        public string ArchivePath
        {
            get
            {
                return (string)GetValue(ArchivePathProperty);
            }
            set
            {
                SetValue(ArchivePathProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ArchivePath" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchivePathProperty = DependencyProperty.Register(
            ArchivePathPropertyName,
            typeof(string),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(""));
        #endregion

        
        #region FileCount
        /// <summary>
        /// The <see cref="FileCount" /> dependency property's name.
        /// </summary>
        private const string FileCountPropertyName = "FileCount";

        /// <summary>
        /// Gets or sets the value of the <see cref="FileCount" />
        /// property. This is a dependency property.
        /// </summary>
        public string FileCount
        {
            get
            {
                return (string)GetValue(FileCountProperty);
            }
            set
            {
                SetValue(FileCountProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="FileCount" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FileCountProperty = DependencyProperty.Register(
            FileCountPropertyName,
            typeof(string),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(""));
        #endregion


        
       #region ArchiveZip
       /// <summary>
        /// The <see cref="ArchiveZip" /> dependency property's name.
        /// </summary>
        private const string ArchiveZipPropertyName = "ArchiveZip";

        /// <summary>
        /// Gets or sets the value of the <see cref="ArchiveZip" />
        /// property. This is a dependency property.
        /// </summary>
        public ZipFile ArchiveZip
        {
            get
            {
                return (ZipFile)GetValue(ArchiveZipProperty);
            }
            set
            {
                SetValue(ArchiveZipProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ArchiveZip" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArchiveZipProperty = DependencyProperty.Register(
            ArchiveZipPropertyName, 
            typeof(ZipFile), 
            typeof(ArchiveInfoModel), 
            new UIPropertyMetadata(null));
            #endregion

        
        #region BufferSize
        /// <summary>
        /// The <see cref="BufferSize" /> dependency property's name.
        /// </summary>
        private const string BufferSizePropertyName = "BufferSize";

        /// <summary>
        /// Gets or sets the value of the <see cref="BufferSize" />
        /// property. This is a dependency property.
        /// </summary>
        public string BufferSize
        {
            get
            {
                return (string)GetValue(BufferSizeProperty);
            }
            set
            {
                SetValue(BufferSizeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="BufferSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BufferSizeProperty = DependencyProperty.Register(
            BufferSizePropertyName,
            typeof(string),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(""));
        #endregion



        string _dbFile = string.Empty;
        public string DataBaseFile { get; set; }
// ReSharper disable ConvertToConstant.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private bool _isKRC2 = true;
// ReSharper restore FieldCanBeMadeReadOnly.Local
// ReSharper restore ConvertToConstant.Local
        
        #region DataBase
        /// <summary>
        /// The <see cref="DataBase" /> dependency property's name.
        /// </summary>
        private const string DataBasePropertyName = "DataBase";

        /// <summary>
        /// Gets or sets the value of the <see cref="DataBase" />
        /// property. This is a dependency property.
        /// </summary>
        public string DataBase
        {
            get
            {
                return (string)GetValue(DataBaseProperty);
            }
            set
            {
                SetValue(DataBaseProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="DataBase" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataBaseProperty = DependencyProperty.Register(
            DataBasePropertyName,
            typeof(string),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(""));
        #endregion
      
        public string InfoFile { get; set; }


        readonly ObservableCollection<DirectoryInfo> _root = new ObservableCollection<DirectoryInfo>();
        readonly ReadOnlyObservableCollection<DirectoryInfo> _readonlyRoot = null;
        public ReadOnlyObservableCollection<DirectoryInfo> Root { get { return _readonlyRoot ?? new ReadOnlyObservableCollection<DirectoryInfo>(_root); } }

        private static string StartupPath
        {
            get
            {
                return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            }
        }

        
        #region RootPath
        /// <summary>
        /// The <see cref="RootPath" /> dependency property's name.
        /// </summary>
        private const string RootPathPropertyName = "RootPath";

        /// <summary>
        /// Gets or sets the value of the <see cref="RootPath" />
        /// property. This is a dependency property.
        /// </summary>
        public DirectoryInfo RootPath
        {
            get
            {
                return (DirectoryInfo)GetValue(RootPathProperty);
            }
            set
            {
                SetValue(RootPathProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="RootPath" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty RootPathProperty = DependencyProperty.Register(
            RootPathPropertyName,typeof(DirectoryInfo),
            typeof(ArchiveInfoModel),
            new UIPropertyMetadata(null));
        #endregion
  
        
       #region LanguageText
       /// <summary>
        /// The <see cref="LanguageText" /> dependency property's name.
        /// </summary>
        private const string LanguageTextPropertyName = "LanguageText";

        /// <summary>
        /// Gets or sets the value of the <see cref="LanguageText" />
        /// property. This is a dependency property.
        /// </summary>
        public string LanguageText
        {
            get
            {
                return (string)GetValue(LanguageTextProperty);
            }
            set
            {
                SetValue(LanguageTextProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="LanguageText" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty LanguageTextProperty = DependencyProperty.Register(
            LanguageTextPropertyName, 
            typeof(string), 
            typeof(ArchiveInfoModel), 
            new UIPropertyMetadata(""));
            #endregion
        
       #region DatabaseText
       /// <summary>
        /// The <see cref="DatabaseText" /> dependency property's name.
        /// </summary>
        private const string DatabaseTextPropertyName = "DatabaseText";

        /// <summary>
        /// Gets or sets the value of the <see cref="DatabaseText" />
        /// property. This is a dependency property.
        /// </summary>
        public string DatabaseText
        {
            get
            {
                return (string)GetValue(DatabaseTextProperty);
            }
            set
            {
                SetValue(DatabaseTextProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="DatabaseText" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DatabaseTextProperty = DependencyProperty.Register(
            DatabaseTextPropertyName, 
            typeof(string), 
            typeof(ArchiveInfoModel), 
            new UIPropertyMetadata(""));
            #endregion




#region Inputs
// ReSharper disable InconsistentNaming
        public ObservableCollection<Item> _inputs = new ObservableCollection<Item>();
// ReSharper restore InconsistentNaming
        readonly ReadOnlyObservableCollection<Item> _readonlyinputs = null;
        public ReadOnlyObservableCollection<Item> Inputs { get { return _readonlyinputs ?? new ReadOnlyObservableCollection<Item>(_inputs); } }

#endregion
#region Outputs
        readonly ObservableCollection<Item> _outputs = new ObservableCollection<Item>();
        readonly ReadOnlyObservableCollection<Item> _readonlyOutputs = null;
        public ReadOnlyObservableCollection<Item> Outputs { get { return _readonlyOutputs ?? new ReadOnlyObservableCollection<Item>(_outputs); } }
        #endregion
#region Anin
        readonly ObservableCollection<Item> _anin = new ObservableCollection<Item>();
        readonly ReadOnlyObservableCollection<Item> _readonlyAnIn = null;
        public ReadOnlyObservableCollection<Item> AnIn { get { return _readonlyAnIn ?? new ReadOnlyObservableCollection<Item>(_anin); } }
#endregion
#region Anout
        readonly ObservableCollection<Item> _anout = new ObservableCollection<Item>();
        readonly ReadOnlyObservableCollection<Item> _readonlyAnOut = null;
        public ReadOnlyObservableCollection<Item> AnOut { get { return _readonlyAnOut ?? new ReadOnlyObservableCollection<Item>(_anout); } }
#endregion
#region Timer
        readonly ObservableCollection<Item> _timer = new ObservableCollection<Item>();
        readonly ReadOnlyObservableCollection<Item> _readonlyTimer = null;
        public ReadOnlyObservableCollection<Item> Timer { get { return _readonlyTimer ?? new ReadOnlyObservableCollection<Item>(_timer); } }
#endregion
#region Flags
        readonly ObservableCollection<Item> _flags = new ObservableCollection<Item>();
        readonly ReadOnlyObservableCollection<Item> _readonlyFlags = null;
        public ReadOnlyObservableCollection<Item> Flags { get { return _readonlyFlags ?? new ReadOnlyObservableCollection<Item>(_flags); } }

#endregion
#region CycFlags
        readonly ObservableCollection<Item> _cycflags = new ObservableCollection<Item>();
        readonly ReadOnlyObservableCollection<Item> _readonlyCycFlags = null;
        public ReadOnlyObservableCollection<Item> CycFlags { get { return _readonlyCycFlags ?? new ReadOnlyObservableCollection<Item>(_cycflags); } }
#endregion
#region Counters
        readonly ObservableCollection<Item> _counter = new ObservableCollection<Item>();
        readonly ReadOnlyObservableCollection<Item> _readonlyCounter = null;
        public ReadOnlyObservableCollection<Item> Counter { get { return _readonlyCounter ?? new ReadOnlyObservableCollection<Item>(_counter); } }
#endregion
        #endregion

        #region Commands

        private RelayCommand _openCommand;
        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new RelayCommand(Open)); }
        }
        private RelayCommand _importCommand;
        public ICommand ImportCommand
        {
            get { return _importCommand ?? (_importCommand = new RelayCommand(Import )); }
        }

        private RelayCommand _loadCommand;
        public ICommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new RelayCommand(GetSignals)); }
        }


        #endregion

        //TODO need to make this into allowing either a folder or a zip file.

        void Import()
        {
            var ofd = new OpenFileDialog { Title = "Select Archive", Filter = "KUKA Archive (*.zip)|*.zip", Multiselect = false };
            ofd.ShowDialog();

            _root.Clear();
            if (!File.Exists(ofd.FileName)) return;
            ArchivePath = ofd.FileName;
            ArchiveZip = new ZipFile(ofd.FileName);

            GetAllLangtextFromDatabase();
            UnpackZip();
            GetFiles(DirectoryPath);
            GetAMInfo();
            GetSignals();
        }

// ReSharper disable UnusedMember.Local
        void ReadZip()
// ReSharper restore UnusedMember.Local
        {
            foreach (var z in ArchiveZip.EntriesSorted.Where(z => z.IsDirectory))
                Console.WriteLine(z.FileName);


            foreach (var e in ArchiveZip.Entries.Where(e => e.IsDirectory))
                Console.WriteLine(e.FileName);


//            RaisePropertyChanged("Root");

        }

        static bool CheckPathExists(string path)
        {
            // if archive path exists, allow to delete or rename
            if (!Directory.Exists(path)) return false;
            throw new NotImplementedException();
            var answer = false;
            var msg =
                new DialogMessage(
                    String.Format("this,The path of {0} \r\n allready exists. Do you want to Delete the path?", path),
                    (result) =>
                    {
                        answer = result != MessageBoxResult.Yes;
                    });
            return answer;

        }
       
        void UnpackZip()
        {
            
          
// ReSharper disable AssignNullToNotNullAttribute
           DirectoryPath= Path.Combine(StartupPath,Path.GetFileNameWithoutExtension(ArchivePath));
// ReSharper restore AssignNullToNotNullAttribute
           var exists= CheckPathExists(DirectoryPath);
           if (exists) return;

                //  This call to ExtractAll() assumes:
                //   - none of the entries are password-protected.
                //   - want to extract all entries to current working directory
                //   - none of the files in the zip already exist in the directory;
                //     if they do, the method will throw.
           ArchiveZip.ExtractAll(DirectoryPath);

           _root.Add(new DirectoryInfo(DirectoryPath));
        }
     
// ReSharper disable UnusedMember.Local
        void GetDirectories()
// ReSharper restore UnusedMember.Local
        {

//            Root = new ObservableCollection<DirectoryRecord>{  new DirectoryRecord { Info = new DirectoryInfo(folder)
           
        }

        void GetAMInfo()
        {
            if (!File.Exists(InfoFile)) return;
            var file = File.ReadAllLines(InfoFile);
            foreach (var sp in file.Select(f => f.Split('=')).Where(sp => sp.Length > 0))
            {
                switch (sp[0])
                {
                    case "Name":
                        Info.ArchiveName = sp[1];
                        break;
                    case "Config":
                        Info.ArchiveConfigType = sp[1];
                        break;
                    case "DiskNo":
                        Info.ArchiveDiskNo = sp[1];
                        break;
                    case "ID":
                        Info.ArchiveID = sp[1];
                        break;
                    case "Date":
                        Info.ArchiveDate = sp[1];
                        break;
                    case "RobName":
                        Info.RobotName = sp[1];
                        break;
                    case "IRSerialNr":
                        Info.RobotSerial = sp[1];
                        break;
                    case "Version":
                        Info.KSSVersion = sp[1];
                        break;
                }
            }
        }

        void GetFlags()
        {
            var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
            const string cmdText = "SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = 'FLAG')";
            using (var oldDbConnection = new OleDbConnection(connectionString))
            {
                oldDbConnection.Open();
                using (var oleDbCommand = new OleDbCommand(cmdText, oldDbConnection))
                {
                    using (var oleDbDataReader = oleDbCommand.ExecuteReader())
                    {
                        while (oleDbDataReader != null && oleDbDataReader.Read())
                        {
                            var temp = oleDbDataReader.GetValue(0).ToString();
                            var sig = new Item(String.Format("$FLAG[{0}]", temp.Substring(8)), oleDbDataReader.GetValue(1).ToString());
                            _flags.Add(sig);
                        }
                    }
                }
            }
            FlagVisibility = Flags.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
          
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
// ReSharper disable UnusedMember.Local
        List<Item> GetValues(string cmd, int index)
// ReSharper restore UnusedMember.Local
        {
            var result = new List<Item>();
            var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
            var cmdText = String.Format("SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = '{0}')",cmd);
            using (var oldDbConnection = new OleDbConnection(connectionString))
            {
                oldDbConnection.Open();
                using (var oleDbCommand = new OleDbCommand(cmdText, oldDbConnection))
                {
                    using (var oleDbDataReader = oleDbCommand.ExecuteReader())
                    {
                        while (oleDbDataReader != null && oleDbDataReader.Read())
                        {
                            var temp = oleDbDataReader.GetValue(0).ToString();
                            var sig = new Item(String.Format("${1}[{0}]", temp.Substring(index),cmd), oleDbDataReader.GetValue(1).ToString());
                            result.Add(sig);
                        }
                    }
                }
            }
            return result;
        }

        void GetTimers()
        {
            var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
            const string cmdText = "SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = 'TIMER')";
            using (var oldDbConnection = new OleDbConnection(connectionString))
            {
                oldDbConnection.Open();
                using (var oleDbCommand = new OleDbCommand(cmdText, oldDbConnection))
                {
                    using (var oleDbDataReader = oleDbCommand.ExecuteReader())
                    {
                        while (oleDbDataReader != null && oleDbDataReader.Read())
                        {
                            var temp = oleDbDataReader.GetValue(0).ToString();
                            var sig = new Item(String.Format("$TIMER[{0}]", temp.Substring(9)), oleDbDataReader.GetValue(1).ToString());
                            _timer.Add(sig);
                        }
                    }
                }
            }
            TimerVisibility = Timer.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            
        }

// ReSharper disable UnusedMember.Local
        void GetSignalsFromDataBase()
// ReSharper restore UnusedMember.Local
        {
            var ofd = new OpenFileDialog { Title = "Select Database", Filter = "KUKA Connection Files (kuka_con.mdb)|kuka_con.mdb|All files (*.*)|*.*", Multiselect = false };
            ofd.ShowDialog();
            LanguageText = string.Empty;

            DataBaseFile = ofd.FileName;
            GetSignals();
        }
    
        void GetSignals()
        {
            if (File.Exists(DataBaseFile))
            {
                var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
                const string cmdText = "SELECT Items.KeyString, Messages.[String] FROM (Items INNER JOIN Messages ON Items.Key_id = Messages.Key_id)WHERE (Items.[Module] = 'IO')";
                using (var oldDbConnection = new OleDbConnection(connectionString))
                {
                    oldDbConnection.Open();
                    using (var oleDbCommand = new OleDbCommand(cmdText, oldDbConnection))
                    {
                        using (var oleDbDataReader = oleDbCommand.ExecuteReader())
                        {
                            while (oleDbDataReader != null && oleDbDataReader.Read())
                            {
                                var temp = oleDbDataReader.GetValue(0).ToString();
                                Item sig;

                                switch (temp.Substring(0, temp.IndexOf("_")))
                                {
                                    case "IN":
                                        sig = new Item(String.Format("$IN[{0}]", temp.Substring(3)), oleDbDataReader.GetValue(1).ToString());

                                        _inputs.Add(sig);
                                        LanguageText += sig + "\r\n";
                                        break;
                                    case "OUT":
                                        sig = new Item(String.Format("$OUT[{0}]", temp.Substring(4)), oleDbDataReader.GetValue(1).ToString());
                                        _outputs.Add(sig);
                                        LanguageText += sig + "\r\n";
                                        break;
                                    case "ANIN":
                                        sig = new Item(String.Format("$ANIN[{0}]", temp.Substring(5)), oleDbDataReader.GetValue(1).ToString());
                                        _anin.Add(sig);
                                        LanguageText += sig + "\r\n";
                                        break;
                                    case "ANOUT":
                                        sig = new Item(String.Format("$ANOUT[{0}]", temp.Substring(6)), oleDbDataReader.GetValue(1).ToString());
                                        _anout.Add(sig);
                                        LanguageText += sig + "\r\n";
                                        break;
                                }

                            }
                        }
                    }
                }
            }
            GetFlags();
            GetTimers();
            GetAllLangtextFromDatabase();
            

        }

        private void GetAllLangtextFromDatabase()
        {
            LanguageText = string.Empty;

            if (!File.Exists(DataBaseFile)) return;
            var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";";
            const string cmdText = "SELECT i.keystring, m.string FROM ITEMS i, messages m where i.key_id=m.key_id and m.language_id=99";
            using (var oldDbConnection = new OleDbConnection(connectionString))
            {
                oldDbConnection.Open();
                using (var oleDbCommand = new OleDbCommand(cmdText, oldDbConnection))
                {
                    using (var oleDbDataReader = oleDbCommand.ExecuteReader())
                    {
                        while (oleDbDataReader != null && oleDbDataReader.Read())
                        {
                            var str1 = oleDbDataReader.GetValue(0).ToString();
                            var str2 = oleDbDataReader.GetValue(1).ToString();
                            DataBase += string.Format("{0} {1}\r\n", str1, str2);
                        }
                    }
                }
            }
        }

// ReSharper disable UnusedMember.Local
        private void ImportFile(string sFile, bool bCsv)
// ReSharper restore UnusedMember.Local
        {
            if (!File.Exists(sFile))
                return;


            // MyProject.Forms.FormMain.LangtextOpen(this.sFileDb);
//            var result = System.Windows.Forms.MessageBox.Show("Delete existing long texts?", "Import File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
 //           if (result == DialogResult.Yes)
   //         {
                //DeleteAllLanguageText
     //       }



            var array = File.ReadAllLines(sFile);
            var text = " ";
            if (bCsv)
                text = ";";


            var progress = new ProgressBarModel { Maximum = array.Length, Value = 0 };
            progress.IsVisible=true;

//            System.Windows.Forms.Application.DoEvents();
            var array2 = array;
            checked
            {
                foreach (var text2 in array2)
                {
                    if (text2.Contains(text))
                    {
                        var array3 = text2.Split(text.ToCharArray(), 2);

                        if (array3[0].StartsWith("$IN[", StringComparison.CurrentCultureIgnoreCase))
                            array3[0] = "IN_" + array3[0].Substring(4, array3[0].Length - 5);
                        else
                            if (array3[0].StartsWith("$OUT[", StringComparison.CurrentCultureIgnoreCase))
                                array3[0] = "OUT_" + array3[0].Substring(5, array3[0].Length - 6);
                            else
                                if (array3[0].StartsWith("$TIMER[", StringComparison.CurrentCultureIgnoreCase))
                                    array3[0] = "TimerText" + array3[0].Substring(7, array3[0].Length - 8);
                                else
                                    if (array3[0].StartsWith("$COUNT_I[", StringComparison.CurrentCultureIgnoreCase))
                                        array3[0] = "CounterText" + array3[0].Substring(9, array3[0].Length - 10);
                                    else
                                        if (array3[0].StartsWith("$FLAG[", StringComparison.CurrentCultureIgnoreCase))
                                            array3[0] = "FlagText" + array3[0].Substring(6, array3[0].Length - 7);
                                        else
                                            if (array3[0].StartsWith("$CYC_FLAG[", StringComparison.CurrentCultureIgnoreCase))
                                                array3[0] = "NoticeText" + array3[0].Substring(10, array3[0].Length - 11);
                                            else
                                                if (array3[0].StartsWith("$ANIN[", StringComparison.CurrentCultureIgnoreCase))
                                                    array3[0] = "ANIN_" + array3[0].Substring(6, array3[0].Length - 7);
                                                else
                                                    if (array3[0].StartsWith("$ANOUT[", StringComparison.CurrentCultureIgnoreCase))
                                                        array3[0] = "ANOUT_" + array3[0].Substring(6, array3[0].Length - 8);
                        //     MyProject.Forms.FormMain.LangtextWrite("IO", array3[0], array3[1]);


                    }
                    progress.Value++;
                }
                progress.IsVisible = false;
                InitGrids();
            }
        }

        void Open()
        {
            var openFileDialog = new OpenFileDialog();
            var openFileDialog2 = openFileDialog;

            openFileDialog2.Filter = "longtext (*.mdb)|*.mdb";


            openFileDialog2.Multiselect = false;
            if (openFileDialog.ShowDialog() != true) return;
            _dbFile = openFileDialog.FileName;
            DataBase = _dbFile;
            InitGrids();
        }
   
#pragma warning disable 649
        string[] _langText;
#pragma warning restore 649

        private void Export(string filename, bool iscsv)

        {
            var text = "";
            var text2 = " ";
            _progress.Maximum = 9551;
            _progress.Value = 0;
            _progress.IsVisible = true;

            var pre = _isKRC2 ? string.Empty : "$";
            var post = _isKRC2 ? "_" : "]";
            if (iscsv)
                text2 = ";";

            checked
            {
                if (_isKRC2)
                {
                    for (var i = 1; i < 4096; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}IN{1}{2}{3}{4}\r\n", pre, post, i, text2, _langText[i]);

                        _progress.Value++;
                    }

                    for (var i = 1; i < 4096; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}OUT{1}{2}{3}{4}\r\n", pre, post, i, text2, _langText[i]);

                        _progress.Value++;
                    }

                    for (var i = 1; i < 32; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}ANIN{1}{2}{3}{4}\r\n", pre, post, i, text2, _langText[i]);

                        _progress.Value++;
                    }

                    for (var i = 1; i < 32; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}ANOUT{1}{2}{3}{4}\r\n", pre, post, i, text2, _langText[i]);

                        _progress.Value++;
                    }
                    for (var i = 1; i < 20; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}{1}{2}{3}{4}\r\n", _isKRC2 ? "TimerText" : "$Timer[", post, i, text2, _langText[i]);

                        _progress.Value++;
                    }

                    for (var i = 1; i < 20; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}{1}{2}{3}{4}\r\n", _isKRC2 ? "CounterText" : "$Counter[", post, i, text2, _langText[i]);

                        _progress.Value++;
                    }

                    for (var i = 1; i < 999; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}{1}{2}{3}{4}\r\n", _isKRC2 ? "FlagText" : "$FLAG[", post, i, text2, _langText[i]);

                        _progress.Value++;
                    }

                    for (var i = 1; i < 256; i++)
                    {
                        if (!String.IsNullOrEmpty(_langText[i]))
                            text = string.Format("{0}{1}{2}{3}{4}\r\n", _isKRC2 ? "NoticeText" : "$CycFlag[", post, i, text2, _langText[i]);

                        _progress.Value++;
                    }

                }

                File.WriteAllText(filename, text);
                _progress.IsVisible = false;
            }

        }

        private void InitGrids()
        {

            _progress.Maximum = 5403;
            _progress.Value = 0;
            _progress.IsVisible = true;

            //            MyProject.Forms.FormMain.LangtextFillBuffer(this.sFileDb);
#pragma warning disable 219
            var num = 1;
#pragma warning restore 219
            checked
            {
                for (var i = 1; i < 4096; i++)
                {
                    _inputs.Add(new Item(String.Format("$IN[{0}]", i), string.Empty));
                    _outputs.Add(new Item(String.Format("$OUT[{0}]", i), string.Empty));

                    _progress.Value++;

                    num++;
                }

                for (var i = 1; i < 32; i++)
                {
                    _anin.Add(new Item(String.Format("$ANIN[{0}]", i), string.Empty));
                    _anout.Add(new Item(String.Format("$ANOUT[{0}]", i), string.Empty));
                    _progress.Value++;
                }

                for (var i = 1; i < 20; i++)
                {
                    _timer.Add(new Item(String.Format("$TIMER[{0}]", i), string.Empty));
                    _counter.Add(new Item(String.Format("$COUNT_I[{0}]", i), string.Empty));
                    _progress.Value++;
                }
                for (var i = 1; i < 999; i++)
                {
                    _flags.Add(new Item(String.Format("$FLAG[{0}]", i), string.Empty));
                    _progress.Value++;
                }
                for (var i = 1; i < 256; i++)
                {
                    _cycflags.Add(new Item(String.Format("$CYCFLAG[{0}]", i), string.Empty));
                    _progress.Value++;
                }

                _progress.IsVisible = false;
            }
        }

        private void GetFiles(string dir)
        {

            // Does AM.INI Exist?
            if (File.Exists(dir + "\\am.ini"))
                InfoFile = dir + "\\am.ini";

            if (File.Exists(dir + "\\C\\KRC\\Data\\kuka_con.mdb"))
                DataBaseFile = dir + "\\C\\KRC\\Data\\kuka_con.mdb";
            if ((File.Exists(InfoFile)) && (File.Exists(DataBaseFile)))
                return;

                foreach (var d in Directory.GetDirectories(dir))
                {
                    foreach (var f in Directory.GetFiles(d))
                    {
                        var fileName = Path.GetFileName(f);
                        if (fileName == null) continue;
                        var file = fileName.ToLower();
                        
#if TRACE
                        Console.WriteLine(file);
#endif
                        switch(file)
                        {
                            case "am.ini":
                                InfoFile = f;
                                break;
                            case "kuka_con.mdb":
                                DataBaseFile = f;
                                break;
                        }
                    }

                    GetFiles(d);
                }
            
        }

    }
}
