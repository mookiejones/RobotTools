using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using miRobotEditor.Core;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core.Classes.Messaging;
using miRobotEditor.Core.Interfaces;
using miRobotEditor.EditorControl;
using miRobotEditor.EditorControl.Interfaces;
using miRobotEditor.EditorControl.Languages;
using Xceed.Wpf.AvalonDock.Layout;

namespace miRobotEditor.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        #region Constructor

        public MainViewModel()
        {
            Instance = this;
            AddNewFile();

            // Register for messages
            Messenger.Default.Register<FileMessage>(this, OpenFile);
            Messenger.Default.Register<OpenFileMessage>(this, OpenVariable);
        }

        private void OpenVariable(OpenFileMessage obj)
        {

            var fileViewModel = Open(obj.Variable.Path);
            fileViewModel.SelectText(obj.Variable);
        }

        private void OpenFile(FileMessage obj)
        {
            OpenFile(obj.Name);
        }

        #endregion

        public string Title
        {
            get
            {
                string fn = ActiveDocument.FilePath ?? string.Empty;
                return ShortenPathname(fn, 100);
            }
        }


        /// <summary>
        ///     Shortens a pathname for display purposes.
        /// </summary>
        /// <param labelName="pathname">The pathname to shorten.</param>
        /// <param labelName="maxLength">The maximum number of characters to be displayed.</param>
        /// <param name="pathname"> </param>
        /// <param name="maxLength"> </param>
        /// <remarks>
        ///     Shortens a pathname by either removing consecutive components of a path
        ///     and/or by removing characters from the end of the filename and replacing
        ///     then with three elipses (...)
        ///     <para>In all cases, the root of the passed path will be preserved in it's entirety.</para>
        ///     <para>
        ///         If a UNC path is used or the pathname and maxLength are particularly short,
        ///         the resulting path may be longer than maxLength.
        ///     </para>
        ///     <para>
        ///         This method expects fully resolved pathnames to be passed to it.
        ///         (Use Path.GetFullPath() to obtain this.)
        ///     </para>
        /// </remarks>
        /// <returns></returns>
        private static string ShortenPathname(string pathname, int maxLength)
        {
            if (pathname.Length <= maxLength) return pathname;

            string root = Path.GetPathRoot(pathname);
            if (root.Length > 3)
                root += Path.DirectorySeparatorChar;

            if (true)
            {
                string[] elements = pathname.Substring(root.Length)
                    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                int filenameIndex = elements.GetLength(0) - 1;

                if (elements.GetLength(0) == 1) // pathname is just a root and filename
                {
                    if (elements[0].Length > 5) // long enough to shorten
                    {
                        // if path is a UNC path, root may be rather long
                        if (root.Length + 6 >= maxLength)
                        {
                            return root + elements[0].Substring(0, 3) + "...";
                        }
                        return pathname.Substring(0, maxLength - 3) + "...";
                    }
                }
                else if ((root.Length + 4 + elements[filenameIndex].Length) > maxLength)
                    // pathname is just a root and filename
                {
                    root += "...\\";

                    int len = elements[filenameIndex].Length;
                    if (len < 6)
                        return root + elements[filenameIndex];

                    if ((root.Length + 6) >= maxLength)
                    {
                        len = 3;
                    }
                    else
                    {
                        len = maxLength - root.Length - 3;
                    }
                    return root + elements[filenameIndex].Substring(0, len) + "...";
                }
                else if (elements.GetLength(0) == 2)
                {
                    return root + "...\\" + elements[1];
                }
                else
                {
                    int len = 0;
                    int begin = 0;

                    for (int i = 0; i < filenameIndex; i++)
                    {
                        if (elements[i].Length <= len) continue;
                        begin = i;
                        len = elements[i].Length;
                    }

                    int totalLength = pathname.Length - len + 3;
                    int end = begin + 1;

                    while (totalLength > maxLength)
                    {
                        if (begin > 0)
                            totalLength -= elements[--begin].Length - 1;

                        if (totalLength <= maxLength)
                            break;

                        if (end < filenameIndex)
                            totalLength -= elements[++end].Length - 1;

                        if (begin == 0 && end == filenameIndex)
                            break;
                    }

                    // assemble final string

                    for (int i = 0; i < begin; i++)
                    {
                        root += elements[i] + '\\';
                    }

                    root += "...\\";

                    for (int i = end; i < filenameIndex; i++)
                    {
                        root += elements[i] + '\\';
                    }

                    return root + elements[filenameIndex];
                }
            }
            return pathname;
        }

        #region Properties

        #region Workspace

        private static MainViewModel _instance;

        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
            set { _instance = value; }
        }

        #endregion

        #region Tools

        private readonly LocalVariablesViewModel _localVariables = null;

        public LocalVariablesViewModel LocalVariables
        {
            get { return _localVariables ?? new LocalVariablesViewModel(); }
        }

        #endregion

        #region ShowSettings

        /// <summary>
        ///     The <see cref="ShowSettings" /> property's name.
        /// </summary>
        private const string ShowSettingsPropertyName = "ShowSettings";

        private bool _showSettings;

        /// <summary>
        ///     Sets and gets the ShowSettings property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool ShowSettings
        {
            get { return _showSettings; }

            set
            {
                if (_showSettings == value)
                {
                    return;
                }

                RaisePropertyChanging(ShowSettingsPropertyName);
                _showSettings = value;
                RaisePropertyChanged(ShowSettingsPropertyName);
            }
        }

        #endregion

        #endregion

        #region Theme

        private string _currentTheme = "Light";

        public string CurrentTheme
        {
            get { return _currentTheme; }
            set
            {
                _currentTheme = value;
                RaisePropertyChanged("Theme");
            }
        }

        #endregion

        #region ShowIO

        /// <summary>
        ///     The <see cref="ShowIO" /> property's name.
        /// </summary>
        public const string ShowIOPropertyName = "ShowIO";

        private bool _showIO;

        /// <summary>
        ///     Sets and gets the ShowIO property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool ShowIO
        {
            get { return _showIO; }

            set
            {
                if (_showIO == value)
                {
                    return;
                }

                RaisePropertyChanging(ShowIOPropertyName);
                _showIO = value;
                RaisePropertyChanged(ShowIOPropertyName);
            }
        }

        #endregion

        #region EnableIO

        /// <summary>
        ///     The <see cref="EnableIO" /> property's name.
        /// </summary>
        public const string EnableIOPropertyName = "EnableIO";

        private bool _enableIO;

        /// <summary>
        ///     Sets and gets the EnableIO property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool EnableIO
        {
            get { return _enableIO; }

            set
            {
                if (_enableIO == value)
                {
                    return;
                }

                RaisePropertyChanging(EnableIOPropertyName);
                _enableIO = value;
                RaisePropertyChanged(EnableIOPropertyName);
            }
        }

        #endregion

        #region Tools

        private readonly IEnumerable<ToolViewModel> _readonlyTools = null;
        private readonly ObservableCollection<ToolViewModel> _tools = new ObservableCollection<ToolViewModel>();

        public IEnumerable<ToolViewModel> Tools
        {
            get { return _readonlyTools ?? new ObservableCollection<ToolViewModel>(_tools); }
        }

        #endregion

        #region IsClosing

        /// <summary>
        ///     The <see cref="IsClosing" /> property's name.
        /// </summary>
        private const string IsClosingPropertyName = "IsClosing";

        private bool _isClosing;

        /// <summary>
        ///     Sets and gets the IsClosing property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsClosing
        {
            get { return _isClosing; }

            set
            {
                if (_isClosing == value)
                {
                    return;
                }

                RaisePropertyChanging(IsClosingPropertyName);
                _isClosing = value;
                RaisePropertyChanged(IsClosingPropertyName);
            }
        }

        #endregion

       
        #region ActiveEditor
        /// <summary>
        /// The <see cref="ActiveDocument" /> property's name.
        /// </summary>
        private const string ActiveEditorPropertyName = "ActiveDocument";

        private IDocument _activeDocument = null;


        private EventHandler ActiveDocumentChanged;
        /// <summary>
        /// Sets and gets the ActiveEditor property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IDocument ActiveDocument
        {
            get
            {
                return _activeDocument;
            }

            set
            {
             //  _activeEditor.ContentId==value.ContentId;

               if (_activeDocument!=null&&(_activeDocument == value))
               {
                   return;
               }

                RaisePropertyChanging(ActiveEditorPropertyName);
                _activeDocument = value;
                _activeDocument.TextBox.Focus();
                RaisePropertyChanged(ActiveEditorPropertyName);
                RaisePropertyChanged("Title");
                           if (ActiveDocumentChanged != null)
                              ActiveDocumentChanged(this, EventArgs.Empty);
            }
        }
        #endregion

        public void BringToFront(string windowName)
        {

            // Does Content Exist Allready?

/*            foreach (
                LayoutAnchorable dd in MainWindow.Instance.DockManager.Layout.Descendents().OfType<LayoutAnchorable>())
            {
                if (dd.Title == windowname)
                    dd.IsActive = true;
            }
 * */
        }

        #region Commands

        #region ExecuteShowIO

        private void ExecuteShowIO()
        {
            ShowIO = !ShowIO;
        }

        //This can probably move to the language class section
        private void ChangeViewAs(object param)
        {
            var lang = param as AbstractLanguageClass;

            if (Equals(ActiveDocument.FileLanguage, lang)) return;

            switch (param.ToString())
            {
                case "ABB":
                    // ReSharper disable RedundantCast
                    ActiveDocument.FileLanguage = (ABB) lang;

                    break;
                case "KUKA":
                    ActiveDocument.FileLanguage = new KUKA();
                    break;
                case "Fanuc":
                    ActiveDocument.FileLanguage = (Fanuc) lang;
                    break;
                case "Kawasaki":
                    ActiveDocument.FileLanguage = (Kawasaki) lang;
                    break;
                    // ReSharper restore RedundantCast
            }

            //                ActiveEditor.TextBox.UpdateVisualText();

            //                DummyDocViewModel.Instance.TextBox.UpdateVisualText();
        }

        private void Exit()
        {
//            MainWindow.Instance.Close();
        }

        internal void Close(DocumentModel fileToClose)
        {
            if (fileToClose.IsDirty)
            {
                
            }
            Editors.Remove(fileToClose);
            RaisePropertyChanged("ActiveEditor");
        }


        public void AddTool(ToolViewModel toolModel)
        {
            if (toolModel == null) return;

            // Does Content Exist Allready?
            foreach (ToolViewModel t in Tools.Where(t => t.Title == toolModel.Title))
            {
                t.IsActive = true;
                return;
            }

            toolModel.IsActive = true;
            _tools.Add(toolModel);
            toolModel.IsActive = true;
            RaisePropertyChanged("Tools");
        }


        [Localizable(false)]
        private void AddTool(object parameter)
        {
            var name = parameter as string;
            ToolViewModel toolModel = null;
            switch (name)
            {
                case "Angle Converter":
//                    toolModel = new AngleConvertorViewModel();
                    break;
                case "Functions":
//                    toolModel = new FunctionViewModel();
                    break;
                case "Explorer":
                    //                    tool.Content = new FileExplorerWindow();
                    break;
                case "Object Browser":
//                    toolModel = new ObjectBrowserViewModel();
                    break;
                case "Output Window":
                    //                toolModel = ServiceLocator.Current.GetInstance<MessageViewModel>();
                    break;
                case "Notes":
                    toolModel = ServiceLocator.Current.GetInstance<NotesViewModel>();
                    break;
                case "ArchiveInfo":
                    //                  toolModel = new ArchiveInfoViewModel();
                    break;
                case "Rename Positions":
                    //TODO Change this
//                    tool.Content = new RenamePositionWindow();
                    break;
                case "Shift":
                    //TODO Change this
                    //                  tool.Content = new ShiftWindow();
                    break;
                case "CleanDat":
                    toolModel = new DatCleanHelper();
                    break;
                default:
                    var msg = new OutputWindowMessage("Not Implemented",
                        String.Format("Add Tool Parameter of {0} not Implemented", name), MsgIcon.Error);
                    Messenger.Default.Send(msg);

                    break;
            }

            if (toolModel != null)
            {
                // Does Content Exist Allready?
                foreach (ToolViewModel t in Tools.Where(t => t.Title == toolModel.Title))
                {
                    t.IsActive = true;
                    return;
                }

                toolModel.IsActive = true;
                _tools.Add(toolModel);
            }
            RaisePropertyChanged("Tools");
        }


        private void ImportRobot()
        {
            AddTool("ArchiveInfo");
        }

        public void ShowAbout()
        {
            // new AboutWindow().ShowDialog();
        }

        /// <summary>
        ///     Event raised when AvalonDock has loaded.
        /// </summary>
        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedParameter.Local
        private void avalonDockHost_AvalonDockLoaded(object sender, EventArgs e)
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
        {
            throw new NotImplementedException();
            // if (System.IO.File.Exists(LayoutFileName))
            // {
            //     //
            //     // If there is already a saved layout file, restore AvalonDock layout from it.
            //     //
            //     avalonDockHost.DockingManager.RestoreLayout(LayoutFileName);
            // }
            // else
            // {
            //     //
            //     // This line of code can be uncommented to get a list of resources.
            //     //
            //     //string[] names = this.GetType().Assembly.GetManifestResourceNames();
            //
            //     //
            //     // Load the default AvalonDock layout from an embedded resource.
            //     //
            //     var assembly = Assembly.GetExecutingAssembly();
            //     using (var stream = assembly.GetManifestResourceStream(DefaultLayoutResourceName))
            //     {
            //         avalonDockHost.DockingManager.RestoreLayout(stream);
            //     }
            // }
        }

        #region IDialogProvider Interface

        /// <summary>
        ///     This method allows the user to select a file to open
        ///     (so the view-model can implement 'Open File' functionality).
        /// </summary>
        public bool UserSelectsFileToOpen(out string filePath)
        {
            var openFileDialog = new OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();
            if (result != null && result.Value)
            {
                filePath = openFileDialog.FileName;
                return true;
            }
            filePath = null;
            return false;
        }


        /// <summary>
        ///     This method allows the user to select a new filename for an existing file
        ///     (so the view-model can implement 'Save As' functionality).
        /// </summary>
        public bool UserSelectsNewFilePath(string oldFilePath, out string newFilePath)
        {
            var saveFileDialog = new SaveFileDialog();
            // saveFileDialog.FileName = ActiveEditor.Filename;

            bool? result = saveFileDialog.ShowDialog();
            if (result.Value)
            {
                newFilePath = saveFileDialog.FileName;
                return true;
            }
            newFilePath = string.Empty;
            return false;
        }

        /// <summary>
        ///     Display an error message dialog box.
        ///     This allows the view-model to display error messages.
        /// </summary>
        public void ErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #region File Handling

        private IDocument OpenFile(string filepath)
        {
            var fileViewModel = Editors.FirstOrDefault(fm => fm.FilePath == filepath);
            if (fileViewModel != null)
                return fileViewModel;


            fileViewModel = AbstractLanguageClass.GetViewModel(filepath);

            if (File.Exists(filepath))
            {
                fileViewModel.Load(filepath);
                // Add file to Recent list
                RecentFileList.Instance.InsertFile(filepath);
                JumpList.AddToRecentCategory(filepath);
            }

            fileViewModel.IsActive = true;
            Editors.Add(fileViewModel);
            ActiveDocument = fileViewModel;
            return fileViewModel;
        }

        #endregion

        #region Commands

        private RelayCommand _showIOCommand;

        public ICommand ShowIOCommand
        {
            get { return _showIOCommand ?? (_showIOCommand = new RelayCommand(ExecuteShowIO, () => true)); }
        }

        #endregion

        #region NewFile

        private RelayCommand _newFileCommand;

        /// <summary>
        ///     Gets the NewFileCommand.
        /// </summary>
        public RelayCommand NewFileCommand
        {
            get
            {
                return _newFileCommand
                       ?? (_newFileCommand = new RelayCommand(
                           AddNewFile));
            }
        }

        #endregion

        #region ShowSettings

        private RelayCommand _showSettingsCommand;

        public ICommand ShowSettingsCommand
        {
            get
            {
                return _showSettingsCommand ??
                       (_showSettingsCommand = new RelayCommand(ExecuteShowSettings));
            }
        }

        #endregion

        #region ShowFindReplace

        private RelayCommand _showFindReplace;

        public ICommand ShowFindReplaceCommand
        {
            get { return _showFindReplace ?? (_showFindReplace = new RelayCommand(ShowFindReplace)); }
        }


        private void ShowFindReplace()
        {
            //          var fnr = new FindandReplaceControl(MainWindow.Instance);
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            //        fnr.ShowDialog().GetValueOrDefault();
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        #endregion

        #region ShowAbout

        private RelayCommand _exitCommand;
        private RelayCommand _showAboutCommand;

        public ICommand ShowAboutCommand
        {
            get { return _showAboutCommand ?? (_showAboutCommand = new RelayCommand(ShowAbout)); }
        }

        #endregion

        #region Exit

        public ICommand ExitCommand
        {
            get { return _exitCommand ?? (_exitCommand = new RelayCommand(Exit)); }
        }

        #endregion

        #region Import

        private RelayCommand<object> _importCommand;

        public RelayCommand<object> ImportCommand
        {
            get
            {
                return _importCommand ??
                       (_importCommand =
                           new RelayCommand<object>(p => ImportRobot(),
                               p => (!(p is LanguageBase) | (p is Fanuc) | (p is Kawasaki) | p == null)));
            }
        }

        #endregion

        #region Open File

        private RelayCommand<object> _openFileCommand;

        /// <summary>
        /// Gets the OpenFileCommand.
        /// </summary>
        public RelayCommand<object> OpenFileCommand
        {
            get
            {
                return _openFileCommand
                    ?? (_openFileCommand = new RelayCommand<object>(ExecuteOpenFileCommand));
            }
        }

        private void ExecuteOpenFileCommand(object parameter)
        {
            OnOpen(parameter);
        }
        #endregion

        #region ChangeViewAs

        private RelayCommand<object> _changeViewAsCommand;

        public RelayCommand<object> ChangeViewAsCommand
        {
            get
            {
                return _changeViewAsCommand ??
                       (_changeViewAsCommand = new RelayCommand<object>(ChangeViewAs));
            }
        }

        #endregion

        #region AddTool

        private RelayCommand<object> _addToolCommand;

        public RelayCommand<object> AddToolCommand
        {
            get { return _addToolCommand ?? (_addToolCommand = new RelayCommand<object>(AddTool, param => true)); }
        }

        #endregion

        #region Show Settings

        private void ExecuteShowSettings()
        {
            ShowSettings = !ShowSettings;
        }

        #endregion

        #region OpenFile

        private ObservableCollection<IDocument> _editors = new ObservableCollection<IDocument>();

        public ObservableCollection<IDocument> Editors
        {
            get { return _editors; }
            set { _editors = value; }
        }

        /// <summary>
        ///     Open file from menu entry
        /// </summary>
        /// <param name="param"></param>
        // ReSharper disable UnusedParameter.Local
        private void OnOpen(object param)
            // ReSharper restore UnusedParameter.Local
        {
            var path = Path.GetDirectoryName(ActiveDocument.FilePath);
            var dlg = new OpenFileDialog
            {
                // Find a way to check for network directory
                //                InitialDirectory="C:\\",
//                Filter = Resources.DefaultFilter,
                Multiselect = true,
                //              FilterIndex = Settings.Default.Filter,
                InitialDirectory = path,
            };

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                Open(dlg.FileName);
            }
        }


        public IDocument Open(string filepath)
        {
            var fileViewModel = OpenFile(filepath, null);
            ActiveDocument = fileViewModel;
            ActiveDocument.IsActive = true;
            return fileViewModel;
        }

        private IDocument OpenFile(string filepath, object o)
        {
            var fileViewModel = Editors.FirstOrDefault(fm => fm.FilePath == filepath);
            if (fileViewModel != null)
                return fileViewModel;


            fileViewModel = AbstractLanguageClass.GetViewModel(filepath);

            fileViewModel.Title = Path.GetFileName(filepath);
            if (File.Exists(filepath))
            {
                fileViewModel.Load(filepath);
                // Add file to Recent list
                RecentFileList.Instance.InsertFile(filepath);
                JumpList.AddToRecentCategory(filepath);
            }
            fileViewModel.IsActive = true;
            Editors.Add(fileViewModel);
            ActiveDocument = fileViewModel;

            return fileViewModel;
        }


        public void OpenFile(IVariable variable)
        {
            IDocument fileViewModel = Open(variable.Path);

            fileViewModel.SelectText(variable);
            //            ActiveEditor.TextBox.SelectText(variable);
        }

        public void AddNewFile()
        {
            Editors.Add(new DocumentModel(null));
            ActiveDocument = Editors.Last();
        }

        public void LoadFile(IList<string> args)
        {
            // Argument 0 is The Path of the main application so i start with argument 1
            for (int i = 1; i < args.Count; i++)
            {
                Open(args[i]);
            }
        }

        #endregion

        #endregion

        #endregion

        #region LayoutStrategy

        private ILayoutUpdateStrategy _layoutInitializer;

        public ILayoutUpdateStrategy LayoutStrategy
        {
            get { return _layoutInitializer ?? (_layoutInitializer = new LayoutInitializer()); }
        }

        #endregion

        /*
        #region Files

        private readonly ObservableCollection<EditorControl.Interfaces.IDocument> _files = new ObservableCollection<EditorControl.Interfaces.IDocument>();
        private readonly ReadOnlyObservableCollection<EditorControl.Interfaces.IDocument> _readonyFiles = null;

        public IEnumerable<EditorControl.Interfaces.IDocument> Files
        {
            get { return _readonyFiles ?? new ReadOnlyObservableCollection<EditorControl.Interfaces.IDocument>(_files); }
        }
        #endregion
        */
    }
}