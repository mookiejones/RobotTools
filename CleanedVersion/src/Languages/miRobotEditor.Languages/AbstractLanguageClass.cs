using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using miRobotEditor.Core;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core.Classes.Messaging;
using miRobotEditor.Core.Interfaces;
using miRobotEditor.GUI.Editor;
using miRobotEditor.Interfaces;
using miRobotEditor.Languages;
using miRobotEditor.UI.Windows;
using miRobotEditor.ViewModel;
using IDocument = miRobotEditor.EditorControl.Interfaces.IDocument;

namespace miRobotEditor.EditorControl.Languages
{
    [Localizable(false)]
    public abstract class AbstractLanguageClass : ViewModelBase
    {
        #region Constructors

        protected AbstractLanguageClass()
        {
            Instance = this;
            //RobotMenuItems=GetMenuItems();
        }

        protected AbstractLanguageClass(string filename)
        {
            DataText = String.Empty;
            SourceText = String.Empty;
            var dir = Path.GetDirectoryName(filename);
            var dirExists = dir != null && Directory.Exists(dir);
            var ext = Path.GetExtension(filename);
            var fn = Path.GetFileNameWithoutExtension(filename);

            if (this is KUKA && ext == ".sub")
            {
                SourceName = Path.GetFileName(filename);
            }
            else
                if ((this is KUKA) && ((ext == ".src") || (ext == ".dat")))
                {
                    SourceName = fn + ".src";
                    DataName = fn + ".dat";
                }
                else
                {
                    SourceName = Path.GetFileName(filename);
                    DataName = string.Empty;
                }

            if (SourceName != null && (dirExists && File.Exists(Path.Combine(dir, SourceName))))
                SourceText += File.ReadAllText(Path.Combine(dir, SourceName));


            if (DataName != null)
                if (dirExists && File.Exists(Path.Combine(dir, DataName)))
                    DataText += File.ReadAllText(Path.Combine(dir, DataName));

            RawText = SourceText + DataText;
            Instance = this;
            RobotMenuItems = GetMenuItems();
        }
        #endregion


        #region Properties
        #region RootPath
        /// <summary>
        /// The <see cref="RootPath" /> property's name.
        /// </summary>
        public const string RootPathPropertyName = "RootPath";

        private DirectoryInfo _rootPath;

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

        #region FileName
        /// <summary>
        /// The <see cref="FileName" /> property's name.
        /// </summary>
        public const string FileNamePropertyName = "FileName";

        private string _filename = String.Empty;

        /// <summary>
        /// Sets and gets the FileName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FileName
        {
            get
            {
                return _filename;
            }

            set
            {
                if (_filename == value)
                {
                    return;
                }

                RaisePropertyChanging(FileNamePropertyName);
                _filename = value;
                RaisePropertyChanged(FileNamePropertyName);
            }
        }
        #endregion

        #region RobotMenuItems
        /// <summary>
        /// The <see cref="RobotMenuItems" /> property's name.
        /// </summary>
        public const string RobotMenuItemsPropertyName = "RobotMenuItems";

        private MenuItem _robotMenuItems;

        /// <summary>
        /// Sets and gets the RobotMenuItems property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public MenuItem RobotMenuItems
        {
            get
            {
                return _robotMenuItems;
            }

            set
            {
                if (_robotMenuItems == value)
                {
                    return;
                }

                RaisePropertyChanging(RobotMenuItemsPropertyName);
                _robotMenuItems = value;
                RaisePropertyChanged(RobotMenuItemsPropertyName);
            }
        }
        #endregion




        public string Name { get { return RobotType == Typlanguage.None ? String.Empty : RobotType.ToString(); } }

        internal string DataName { get; private set; }
        internal string SourceName { get; private set; }

        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public static int Progress { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local

        private readonly ObservableCollection<IVariable> _objectBrowserVariables = new ObservableCollection<IVariable>();
        readonly ReadOnlyObservableCollection<IVariable> _readOnlyBrowserVariables = null;
        public ReadOnlyObservableCollection<IVariable> ObjectBrowserVariable { get { return _readOnlyBrowserVariables ?? new ReadOnlyObservableCollection<IVariable>(_objectBrowserVariables); } }

        private readonly ObservableCollection<MenuItem> _menuItems = new ObservableCollection<MenuItem>();
        readonly ReadOnlyObservableCollection<MenuItem> _readonlyMenuItems = null;
        public IEnumerable<MenuItem> MenuItems { get { return _readonlyMenuItems ?? new ReadOnlyObservableCollection<MenuItem>(_menuItems); } }

        readonly List<System.IO.FileInfo> _files = new List<System.IO.FileInfo>();
        readonly ReadOnlyCollection<System.IO.FileInfo> _readOnlyFiles = null;
        public ReadOnlyCollection<System.IO.FileInfo> Files { get { return _readOnlyFiles ?? new ReadOnlyCollection<System.IO.FileInfo>(_files); } }

        /// <summary>
        /// Text of _files For searching
        /// </summary>
        internal string RawText { private get; set; }

        private static AbstractLanguageClass Instance { get; set; }
        internal string SourceText { get; private set; }
        internal string DataText { get; private set; }

        // ReSharper disable once UnusedMember.Global
        public string SnippetPath
        {
            get { return ".\\Editor\\Config _files\\Snippet.xml"; }
        }

        // ReSharper disable once UnusedMember.Global
        protected string Intellisense
        {
            get { return String.Concat(RobotType.ToString(), "Intellisense.xml"); }
        }

        // ReSharper disable once UnusedMember.Global
        protected string SnippetFilePath
        {
            get { return String.Concat(RobotType.ToString(), "Snippets.xml"); }
        }

        internal string Filename { get; set; }

        protected string ConfigFilePath
        {
            get { return String.Concat(RobotType.ToString(), "Config.xml"); }
        }

        internal string SyntaxHighlightFilePath
        {
            get { return String.Concat(RobotType.ToString(), "Highlight.xshd"); }
        }

        internal string StyleFilePath
        {
            get { return String.Concat(RobotType.ToString(), "Style.xshd"); }
        }

        public static int FileCount { get; private set; }

        internal readonly List<IVariable> _allVariables = new List<IVariable>();
        readonly ReadOnlyCollection<IVariable> _readOnlyAllVariables = null;
        public ReadOnlyCollection<IVariable> AllVariables { get { return _readOnlyAllVariables ?? new ReadOnlyCollection<IVariable>(_allVariables); } }

        List<IVariable> _functions = new List<IVariable>();
        readonly ReadOnlyCollection<IVariable> _readOnlyFunctions = null;
        public ReadOnlyCollection<IVariable> Functions { get { return _readOnlyFunctions ?? new ReadOnlyCollection<IVariable>(_functions); } }

        List<IVariable> _fields = new List<IVariable>();
        readonly ReadOnlyCollection<IVariable> _readOnlyFields = null;
        public ReadOnlyCollection<IVariable> Fields { get { return _readOnlyFields ?? new ReadOnlyCollection<IVariable>(_fields); } }

        List<IVariable> _positions = new List<IVariable>();
        readonly ReadOnlyCollection<IVariable> _readOnlypositions = null;
        public ReadOnlyCollection<IVariable> Positions { get { return _readOnlypositions ?? new ReadOnlyCollection<IVariable>(_positions); } }


        readonly List<IVariable> _enums = new List<IVariable>();
        readonly ReadOnlyCollection<IVariable> _readOnlyenums = null;
        public ReadOnlyCollection<IVariable> Enums { get { return _readOnlyenums ?? new ReadOnlyCollection<IVariable>(_enums); } }


        readonly List<IVariable> _structures = new List<IVariable>();
        readonly ReadOnlyCollection<IVariable> _readOnlystructures = null;
        public ReadOnlyCollection<IVariable> Structures { get { return _readOnlystructures ?? new ReadOnlyCollection<IVariable>(_structures); } }

        readonly List<IVariable> _signals = new List<IVariable>();
        readonly ReadOnlyCollection<IVariable> _readOnlysignals = null;
        public ReadOnlyCollection<IVariable> Signals { get { return _readOnlysignals ?? new ReadOnlyCollection<IVariable>(_signals); } }

        #endregion


        #region Abstract
        #region Abstract Members

        public abstract string CommentChar { get; }
        #endregion

        #region Abstract Properties

        public abstract List<string> SearchFilters { get; }

        internal abstract Typlanguage RobotType { get; }
        protected abstract string ShiftRegex { get; }

        public abstract Regex MethodRegex { get; }

        /// <summary>
        /// Regular Expression for finding Fields
        /// <remarks> Used in Editor.FindBookmarks</remarks>
        /// </summary>
        public abstract Regex FieldRegex { get; }

        public abstract Regex EnumRegex { get; }

        public abstract Regex XYZRegex { get; }

        public abstract Regex StructRegex { get; }
        public abstract Regex SignalRegex { get; }

        internal abstract bool IsFileValid(System.IO.FileInfo file);

        /// <summary>
        /// Regular Expression for Functions
        /// </summary>
        internal abstract string FunctionItems { get; }

        internal abstract IList<ICompletionData> CodeCompletion { get; }
        internal abstract AbstractFoldingStrategy FoldingStrategy { get; set; }

        #endregion


        #region Abstract Methods
        public abstract DocumentViewModel GetFile(string filename);

        // ReSharper disable once InconsistentNaming
        public abstract string ExtractXYZ(string positionstring);
        //TODO Need to figure a way to use multiple extensions
        /// <summary>
        /// Source file extension
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        internal abstract string SourceFile { get; }

        internal abstract string FoldTitle(FoldingSection section, TextDocument doc);
        #endregion
        #endregion

        private MenuItem GetMenuItems()
        {
            var rd = new ResourceDictionary { Source = new Uri("/miRobotEditor;component/Templates/MenuDictionary.xaml", UriKind.RelativeOrAbsolute) };
            var i = rd[RobotType + "Menu"] as MenuItem ?? new MenuItem();
            return i;
        }

        public static DocumentViewModel GetViewModel(string filepath)
        {
            if (!String.IsNullOrEmpty(filepath))
            {
                var extension = Path.GetExtension(filepath);
                switch (extension.ToLower())
                {
                    case ".as":
                    case ".pg":
                        return new DocumentViewModel(filepath, new Kawasaki(filepath));
                    case ".src":
                    case ".dat":
                        return GetKukaViewModel(filepath);
                    case ".rt":
                    case ".sub":
                    case ".kfd":
                        return new DocumentViewModel(filepath, new KUKA(filepath));
                    case ".mod":
                    case ".prg":
                        return new DocumentViewModel(filepath, new ABB(filepath));
                    case ".bas":
                        return new DocumentViewModel(filepath, new VBA(filepath));
                    case ".ls":
                        return new DocumentViewModel(filepath, new Fanuc(filepath));
                    default:
                        return new DocumentViewModel(filepath, new LanguageBase(filepath));
                }
            }
            return new DocumentViewModel(null, new LanguageBase(filepath));
        }


        static DocumentViewModel GetKukaViewModel(string filepath)
        {
            var dir = Path.GetDirectoryName(filepath);
            var file = Path.GetFileNameWithoutExtension(filepath);
            Debug.Assert(file != null, "file != null");
            Debug.Assert(dir != null, "dir != null");
            file = Path.Combine(dir, file);
            var datExists = File.Exists(file + ".dat");
            var srcExists = File.Exists(file + ".src");

            if (datExists && srcExists)
                return new KukaViewModel(file + ".src", new KUKA(file + ".src"));

            return new DocumentViewModel(filepath, new KUKA(filepath));
            //Need to see if both paths exist
        }
        /// <summary>
        /// Strips Comment Character from string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual string CommentReplaceString(string text)
        {
            var pattern = String.Format("^([ ]*)([{0}]*)([^\r\n]*)", CommentChar);
            var rgx = new Regex(pattern);
            var m = rgx.Match(text);
            if (m.Success)
            {
                return m.Groups[1] + m.Groups[3].ToString();
            }
            return text;
        }

        public virtual int CommentOffset(string text)
        {
            //TODO Create Result Regex
            var rgx = new Regex(@"(^[\s]+)");
            {
                var m = rgx.Match(text);
                if (m.Success)
                    return m.Groups[1].Length;
                //return m.Groups[1].ToString()+ m.Groups[2].ToString();
            }


            return 0;
        }

        /// <summary>
        /// Trims Line and Then Returns if first Character is a comment Character
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLineCommented(string text)
        {
            return text.Trim().IndexOf(CommentChar, StringComparison.Ordinal).Equals(0);
        }




        #region Folding Section


        static bool IsValidFold(string text, string s, string e)
        {
            text = text.Trim();
            var bSp = text.StartsWith(s);
            var bEp = text.StartsWith(e);

            if (!(bSp | bEp)) return false;

            var lookfor = bSp ? s : e;

            //TODO Come Back and fix this
            if (text.Substring(text.IndexOf(lookfor, StringComparison.Ordinal) + lookfor.Length).Length == 0) return true;

            var cAfterString = text.Substring(text.IndexOf(lookfor, StringComparison.Ordinal) + lookfor.Length, 1);


            var cc = Convert.ToChar(cAfterString);
            var isLetter = Char.IsLetterOrDigit(cc);

            return (!isLetter);
        }

        protected static IEnumerable<LanguageFold> CreateFoldingHelper(ITextSource document, string startFold, string endFold, bool defaultclosed)
        {
            var newFoldings = new List<LanguageFold>();
            var startOffsets = new Stack<int>();
            var doc = (document as TextDocument);
            endFold = endFold.ToLower();
#pragma warning disable 219
            var err = 0;
#pragma warning restore 219

            //TODO Instead of Parsing through lines, I may want to search the textrope

            if (doc != null)
                foreach (var dd in doc.Lines)
                {
                    var line = doc.GetLineByNumber(dd.LineNumber);
                    var text = doc.GetText(line.Offset, line.Length).ToLower();
                    var eval = text.TrimStart();

                    try
                    {

                        if (!IsValidFold(text, startFold, endFold))
                            continue;

                        if (eval.StartsWith(startFold))
                        {
                            startOffsets.Push(line.Offset);
                            continue;
                        }


                        if (eval.StartsWith(endFold) && startOffsets.Count > 0)
                        {
                            // Might Be for EndFolds
                            bool valid;
                            if (endFold == "end")
                            {
                                if (text.Length == endFold.Length)
                                    valid = true;
                                else
                                {
                                    var ee = text.ToCharArray(endFold.Length, 1);
                                    valid = !char.IsLetterOrDigit(ee[0]);
                                }
                            }
                            else
                                valid = true; // Not an End Statement
                            if (valid)
                            {
                                //Add a new folder to the list
                                var s = startOffsets.Pop();

                                var e = line.Offset + text.Length;

                                var str = doc.GetText(s + startFold.Length + 1, line.Offset - s - endFold.Length);


                                var nf = new LanguageFold(s, e, str, startFold, endFold, defaultclosed);
                                newFoldings.Add(nf);
                            }
                        }
                        else
                            err++;

                    }
                    // ReSharper disable EmptyGeneralCatchClause
                    catch (Exception)
                    // ReSharper restore EmptyGeneralCatchClause
                    {
                        //TODO May want to put in messaging later about the folds
                        //                        MessageViewModel.AddError("AbstractLanguageClass.CreateFoldingHelper", ex);
                    }

                }

            return newFoldings;
        }

        #endregion

        #region Shift Section

        /// <summary>
        /// Shift Program Fuction
        /// <remarks> Uses RegexString variable to get positions and shift</remarks>
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public ShiftClass ShiftProgram(IDocument doc, ShiftWindow shift)
        {
            if (doc is KukaViewModel)
            {
                var d = doc as KukaViewModel;

                var result = new ShiftClass { Source = ShiftProgram(d.Source, shift) };

                if (!(string.IsNullOrEmpty(d.Data.Text)))
                    result.Data = ShiftProgram(d.Data, shift);

                return result;
            }
            else
            {
                var result = new ShiftClass { Source = ShiftProgram(doc.TextBox, shift) };
                return result;
            }
        }

        // ReSharper disable UnusedParameter.Local
        private string ShiftProgram(Editor doc, ShiftModel shift)
        // ReSharper restore UnusedParameter.Local
        {

            //TODO: Need to put all of this into a thread.
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var shiftvalX = Convert.ToDouble(shift.DiffValues.X);
            var shiftvalY = Convert.ToDouble(shift.DiffValues.Y);
            var shiftvalZ = Convert.ToDouble(shift.DiffValues.Z);


            var r = new Regex(ShiftRegex, RegexOptions.IgnoreCase);

            var matches = r.Matches(doc.Text);
            var count = matches.Count;


            // get divisible value for progress update
            Double prog = 0;

            double increment = (count > 0) ? 100 / count : count;


            // doc.SuspendLayout();
            foreach (Match m in r.Matches(doc.Text))
            {

                prog = prog + increment;

                // ReSharper disable UnusedVariable
                var xf = Convert.ToDouble(m.Groups[3].Value) + shiftvalX;

                var yf = Convert.ToDouble(m.Groups[4].Value) + shiftvalY;
                var zf = Convert.ToDouble(m.Groups[5].Value) + shiftvalZ;
                // ReSharper restore UnusedVariable
                switch (RobotType)
                {
                    case Typlanguage.KUKA:
                        doc.ReplaceAll();
                        break;
                    case Typlanguage.ABB:
                        doc.ReplaceAll();
                        break;
                }
            }
            //           doc.ResumeLayout();

            Thread.Sleep(500);


            stopwatch.Stop();
#if TRACE
            Console.WriteLine("{0}ms to parse shift", stopwatch.ElapsedMilliseconds);
#endif

            return doc.Text;
        }

        #endregion




        // Try to Find Variables

        #region Automatic ObjectBrowser
        string _rootName = string.Empty;
        //TODO Split this up for a robot by robot basis
        private const string TargetDirectory = "KRC";
        bool _rootFound;
        public void GetRootDirectory(string dir)
        {
            //Search Backwards from current point to root directory
            var dd = new DirectoryInfo(dir);

            // Cannot Parse Directory
            if (dd.Name == dd.Root.Name) _rootFound = true;

            try
            {
                while (dd.Parent != null && ((!_rootFound) && (dd.Parent.Name != TargetDirectory)))
                {
                    GetRootDirectory(dd.Parent.FullName);
                }


                if (_rootFound) return;

                if (dd.Parent != null)
                    if (dd.Parent.Parent != null)
                        if (dd.Parent.Parent.Parent != null)
                            _rootName = dd.Parent != null && dd.Parent.Parent.Name != "C" ? dd.Parent.Parent.FullName : dd.Parent.Parent.Parent.FullName;

                var r = new DirectoryInfo(_rootName);

                var f = r.GetDirectories();


                if (f.Length < 1) return;
                if ((f[0].Name == "C") && (f[1].Name == "KRC"))
                    _rootName = r.FullName;

                _rootFound = true;

                GetRootFiles(_rootName);
                FileCount = Files.Count;

                GetVariables();
                _allVariables.AddRange(Functions);
                _allVariables.AddRange(Fields);
                _allVariables.AddRange(Positions);
                _allVariables.AddRange(Signals);


            }
            catch (Exception ex)
            {
                var msg = new ErrorMessage("Get Root Directory", ex);
                Messenger.Default.Send(msg);


            }

            // Need to get further to the root so that i can interrogate system files as well.
        }


        private IOViewModel _ioModel;
        public IOViewModel IOModel { get { return _ioModel; } set { _ioModel = value; RaisePropertyChanged(); } }
        private string _kukaCon;

        private void GetRootFiles(string dir)
        {
            foreach (var d in Directory.GetDirectories(dir))
            {
                foreach (var f in Directory.GetFiles(d))
                {
                    try
                    {
                        var file = new System.IO.FileInfo(f);
                        if (file.Name.ToLower() == "kuka_con.mdb")
                            _kukaCon = file.FullName;
                        _files.Add(file);
                    }
                    catch (Exception e)
                    {
                        var msg = new ErrorMessage("Error When Getting Files for Object Browser", e);
                        Messenger.Default.Send(msg);
                    }

                }

                GetRootFiles(d);
            }
        }


        #region Properties for Background Worker and StatusBar

        #region BWProgress
        /// <summary>
        /// The <see cref="BWProgress" /> property's name.
        /// </summary>
        public const string BWProgressPropertyName = "BWProgress";

        private int _bwProgress;

        /// <summary>
        /// Sets and gets the BWProgress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int BWProgress
        {
            get
            {
                return _bwProgress;
            }

            set
            {
                if (_bwProgress == value)
                {
                    return;
                }

                RaisePropertyChanging(BWProgressPropertyName);
                _bwProgress = value;
                RaisePropertyChanged(BWProgressPropertyName);
            }
        }
        #endregion

        #region BWFilesMin
        /// <summary>
        /// The <see cref="BWFilesMin" /> property's name.
        /// </summary>
        public const string BWFilesMinPropertyName = "BWFilesMin";

        private int _bwFilesMin;

        /// <summary>
        /// Sets and gets the BWFilesMin property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int BWFilesMin
        {
            get
            {
                return _bwFilesMin;
            }

            set
            {
                if (_bwFilesMin == value)
                {
                    return;
                }

                RaisePropertyChanging(BWFilesMinPropertyName);
                _bwFilesMin = value;
                RaisePropertyChanged(BWFilesMinPropertyName);
            }
        }
        #endregion

        #region BWFilesMax
        /// <summary>
        /// The <see cref="BWFilesMax" /> property's name.
        /// </summary>
        public const string BWFilesMaxPropertyName = "BWFilesMax";

        private int _bwFilesMax;

        /// <summary>
        /// Sets and gets the BWFilesMax property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int BWFilesMax
        {
            get
            {
                return _bwFilesMax;
            }

            set
            {
                if (_bwFilesMax == value)
                {
                    return;
                }

                RaisePropertyChanging(BWFilesMaxPropertyName);
                _bwFilesMax = value;
                RaisePropertyChanged(BWFilesMaxPropertyName);
            }
        }
        #endregion

        #region BWProgressVisibility
        /// <summary>
        /// The <see cref="BWProgressVisibility" /> property's name.
        /// </summary>
        public const string BWProgressVisibilityPropertyName = "BWProgressVisibility";

        private Visibility _bwProgressVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the BWProgressVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility BWProgressVisibility
        {
            get
            {
                return _bwProgressVisibility;
            }

            set
            {
                if (_bwProgressVisibility == value)
                {
                    return;
                }

                RaisePropertyChanging(BWProgressVisibilityPropertyName);
                _bwProgressVisibility = value;
                RaisePropertyChanged(BWProgressVisibilityPropertyName);
            }
        }
        #endregion
        BackgroundWorker _bw;

        #endregion

        void GetVariables()
        {
            _bw = new BackgroundWorker();
            BWProgressVisibility = Visibility.Visible;
            _bw.DoWork += backgroundVariableWorker_DoWork;
            _bw.WorkerReportsProgress = true;
            _bw.ProgressChanged += _bw_ProgressChanged;
            _bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            _bw.RunWorkerAsync();
        }

        void _bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BWProgress = e.ProgressPercentage;
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RaisePropertyChanged("Functions");
            RaisePropertyChanged("Fields");
            RaisePropertyChanged("Files");
            RaisePropertyChanged("Positions");
            BWProgressVisibility = Visibility.Collapsed;

            // Dispose of Background worker
            _bw = null;
            //TODO Open Variable Monitor
            Workspace.Instance.EnableIO = File.Exists(_kukaCon);
            IOModel = new IOViewModel(_kukaCon);
        }

        void backgroundVariableWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BWFilesMax = Files.Count;
            var i = 0;
            _functions = new List<IVariable>();
            _fields = new List<IVariable>();
            _positions = new List<IVariable>();
            foreach (var f in Files)
            {

                // Check to see if file is ok to check for values
                if (IsFileValid(f))
                {
                    _functions.AddRange(FindMatches(MethodRegex, Global.ImgMethod, f.FullName));
                    _structures.AddRange(FindMatches(StructRegex, Global.ImgStruct, f.FullName));
                    _fields.AddRange(FindMatches(FieldRegex, Global.ImgField, f.FullName));
                    _signals.AddRange(FindMatches(SignalRegex, Global.ImgSignal, f.FullName));
                    _enums.AddRange(FindMatches(EnumRegex, Global.ImgEnum, f.FullName));
                    _positions.AddRange(FindMatches(XYZRegex, Global.ImgXyz, f.FullName));
                }
                i++;
                _bw.ReportProgress(i);
            }
        }


        //TODO Signal Path for KUKARegex currently displays linear motion
        private static IEnumerable<IVariable> FindMatches(Regex matchstring, string imgPath, string filepath)
        {

            //TODO Go Back and Change All Regex to be case insensitive

            var result = new List<IVariable>();
            try
            {
                var text = File.ReadAllText(filepath);

                // Dont Include Empty Values
                if (String.IsNullOrEmpty(matchstring.ToString())) return result;

                var m = matchstring.Match(text);

                while (m.Success)
                {
                    result.Add(new Variable
                    {
                        Declaration = m.Groups[0].ToString(),
                        Offset = m.Index,
                        Type = m.Groups[1].ToString(),
                        Name = m.Groups[2].ToString(),
                        Value = m.Groups[3].ToString(),
                        Path = filepath,
                        Icon = Utilities.LoadBitmap(imgPath)
                    });
                    m = m.NextMatch();
                }
            }
            catch (Exception ex)
            {
                MessageViewModel.AddError("Find Matches", ex);
            }

            return result;
        }
        #endregion

    }



}

