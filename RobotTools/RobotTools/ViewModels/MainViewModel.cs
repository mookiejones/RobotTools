using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RobotTools.ViewModels.Base;
using RobotTools.ViewModels.MRU;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace RobotTools.ViewModels
{
    partial class MainViewModel: ObservableValidator
    {

       public MainViewModel()
        {
            _files = new ObservableCollection<FileBaseViewModel>();
            _files.Add(new StartPageViewModel());
        }

        static MainViewModel _this = new MainViewModel();

        public static MainViewModel This
        {
            get { return _this; }
        }
        ObservableCollection<FileBaseViewModel> _files = null;
        ReadOnlyObservableCollection<FileBaseViewModel> _readonyFiles = null;
        public ReadOnlyObservableCollection<FileBaseViewModel> Files
        {
            get
            {
                if (_readonyFiles == null)
                    _readonyFiles = new ReadOnlyObservableCollection<FileBaseViewModel>(_files);

                return _readonyFiles;
            }
        }

        ToolViewModel[] _tools = null;

        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (_tools == null)
                    _tools = new ToolViewModel[] { RecentFiles, FileStats };
                return _tools;
            }
        }

        FileStatsViewModel _fileStats = null;
        public FileStatsViewModel FileStats
        {
            get
            {
                if (_fileStats == null)
                    _fileStats = new FileStatsViewModel();

                return _fileStats;
            }
        }

        #region OpenCommand
        RelayCommand<object> _openCommand = null;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand<object>((p) => OnOpen(p), (p) => CanOpen(p));
                }

                return _openCommand;
            }
        }

        private bool CanOpen(object parameter)
        {
            return true;
        }

        private void OnOpen(object parameter)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                var fileViewModel = Open(dlg.FileName);
                ActiveDocument = fileViewModel;
            }
        }

        public FileViewModel Open(string filepath)
        {
            List<FileViewModel> filesFileViewModel = _files.OfType<FileViewModel>().ToList();

            // Verify whether file is already open in editor, and if so, show it
            var fileViewModel = filesFileViewModel.FirstOrDefault(fm => fm.FilePath == filepath);

            if (fileViewModel != null)
            {
                ActiveDocument = fileViewModel; // File is already open so show it

                return fileViewModel;
            }

            fileViewModel = new FileViewModel(filepath);
            _files.Add(fileViewModel);
            RecentFiles.AddNewEntryIntoMRU(filepath);

            return fileViewModel;
        }

        #endregion

        #region NewCommand
        RelayCommand<object> _newCommand = null;
        public ICommand NewCommand
        {
            get
            {
                if (_newCommand == null)
                {
                    _newCommand = new RelayCommand<object>((p) => OnNew(p), (p) => CanNew(p));
                }

                return _newCommand;
            }
        }

        private bool CanNew(object parameter)
        {
            return true;
        }

        private void OnNew(object parameter)
        {
            _files.Add(new FileViewModel());
            ActiveDocument = _files.Last();
        }

        #endregion

        #region ActiveDocument

        private FileBaseViewModel _activeDocument = null;
        public FileBaseViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    OnPropertyChanged("ActiveDocument");
                    if (ActiveDocumentChanged != null)
                        ActiveDocumentChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ActiveDocumentChanged;

        #endregion

        internal void Close(FileBaseViewModel doc)
        {
            {
                FileViewModel fileToClose = doc as FileViewModel;

                if (fileToClose != null)
                {
                    if (fileToClose.IsDirty)
                    {
                        var res = MessageBox.Show(string.Format("Save changes for file '{0}'?", fileToClose.FileName), "AvalonDock Test App", MessageBoxButton.YesNoCancel);
                        if (res == MessageBoxResult.Cancel)
                            return;

                        if (res == MessageBoxResult.Yes)
                        {
                            Save(fileToClose);
                        }
                    }

                    _files.Remove(fileToClose);

                    return;

                }
            }

            {
                StartPageViewModel s = doc as StartPageViewModel;
                if (s != null)
                {
                    _files.Remove(doc);

                    if (_files.Count == 0)
                        ActiveDocument = null;
                    else
                        ActiveDocument = _files[0];

                    return;
                }
            }

        }

        internal void Save(FileViewModel fileToSave, bool saveAsFlag = false)
        {
            if (fileToSave.FilePath == null || saveAsFlag)
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog().GetValueOrDefault())
                    fileToSave.SetFileName(dlg.SafeFileName);
            }

            File.WriteAllText(fileToSave.FilePath, fileToSave.TextContent);

            if (ActiveDocument != null)
            {
                if (ActiveDocument is FileViewModel)
                {
                    ((FileViewModel)ActiveDocument).IsDirty = false;
                }
            }
        }

        #region Recent File List Pin Unpin Commands
        /// <summary>
        /// This property manages the data visible in the Recent Files ViewModel.
        /// </summary>
        private RecentFilesViewModel _recentFiles = null;
        public RecentFilesViewModel RecentFiles
        {
            get
            {
                if (_recentFiles == null)
                    _recentFiles = new RecentFilesViewModel();

                return _recentFiles;
            }
        }

        private void PinCommand_Executed(object o, ExecutedRoutedEventArgs e)
        {
            MRUEntryVM cmdParam = o as MRUEntryVM;

            if (cmdParam == null)
                return;

            if (e != null)
                e.Handled = true;

            RecentFiles.MruList.PinUnpinEntry(!cmdParam.IsPinned, cmdParam);
        }

        private void AddMRUEntry_Executed(object o, ExecutedRoutedEventArgs e)
        {
            MRUEntryVM cmdParam = o as MRUEntryVM;

            if (cmdParam == null)
                return;

            if (e != null)
                e.Handled = true;

            RecentFiles.MruList.AddMRUEntry(cmdParam);
        }

        private void RemoveMRUEntry_Executed(object o, ExecutedRoutedEventArgs e)
        {
            MRUEntryVM cmdParam = o as MRUEntryVM;

            if (cmdParam == null)
                return;

            if (e != null)
                e.Handled = true;

            RecentFiles.MruList.RemovePinEntry(cmdParam);
        }
        #endregion Recent File List Pin Unpin Commands

        /// <summary>
        /// Bind a window to some commands to be executed by the viewmodel.
        /// </summary>
        /// <param name="win"></param>
        public void InitCommandBinding(Window win)
        {
            win.CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
            (s, e) =>
            {
                OnNew(null);
            }));

            win.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open,
            (s, e) =>
            {
                OnOpen(null);
            }));

            win.CommandBindings.Add(new CommandBinding(AppCommand.LoadFile,
            (s, e) =>
            {
                if (e == null)
                    return;

                string filename = e.Parameter as string;

                if (filename == null)
                    return;

                Open(filename);
            }));

            win.CommandBindings.Add(new CommandBinding(AppCommand.PinUnpin,
            (s, e) =>
            {
                PinCommand_Executed(e.Parameter, e);
            }));

            win.CommandBindings.Add(new CommandBinding(AppCommand.RemoveMruEntry,
            (s, e) =>
            {
                RemoveMRUEntry_Executed(e.Parameter, e);
            }));

            win.CommandBindings.Add(new CommandBinding(AppCommand.AddMruEntry,
            (s, e) =>
            {
                AddMRUEntry_Executed(e.Parameter, e);
            }));

            win.CommandBindings.Add(new CommandBinding(AppCommand.BrowseURL,
            (s, e) =>
            {
                Process.Start(new ProcessStartInfo("http://Edi.codeplex.com"));
            }));

            win.CommandBindings.Add(new CommandBinding(AppCommand.ShowStartPage,
            (s, e) =>
            {
                StartPageViewModel spage = GetStartPage(true);

                if (spage != null)
                {
                    ActiveDocument = spage;
                }
            }));
        }

        /// <summary>
        /// Construct and add a new <seealso cref="StartPageViewModel"/> to intenral
        /// list of documents, if none is already present, otherwise return already
        /// present <seealso cref="StartPageViewModel"/> from internal document collection.
        /// </summary>
        /// <param name="CreateNewViewModelIfNecessary"></param>
        /// <returns></returns>
        internal StartPageViewModel GetStartPage(bool CreateNewViewModelIfNecessary)
        {
            List<StartPageViewModel> l = _files.OfType<StartPageViewModel>().ToList();

            if (l.Count == 0)
            {
                if (CreateNewViewModelIfNecessary == false)
                    return null;
                else
                {
                    StartPageViewModel s = new StartPageViewModel();
                    _files.Add(s);

                    return s;
                }
            }

            return l[0];
        }


    }
}
