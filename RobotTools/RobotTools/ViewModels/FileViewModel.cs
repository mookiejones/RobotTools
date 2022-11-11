using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Windows.Input;

namespace RobotTools.ViewModels
{
    class FileViewModel : Base.FileBaseViewModel
    {
        public FileViewModel(string filePath)
        {
            FilePath = filePath;
            Title = FileName;
        }

        public FileViewModel()
        {
            IsDirty = true;
            Title = FileName;
        }

        #region FilePath
        private string _filePath = null;
        override public string FilePath
        {
            get { return _filePath; }
            protected set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged("FilePath");
                    OnPropertyChanged("FileName");
                    OnPropertyChanged("Title");

                    if (File.Exists(_filePath))
                    {
                        _textContent = File.ReadAllText(_filePath);
                        ContentId = _filePath;
                    }
                }
            }
        }
        #endregion

        public string FileName
        {
            get
            {
                if (FilePath == null)
                    return "Noname" + (IsDirty ? "*" : "");

                return System.IO.Path.GetFileName(FilePath) + (IsDirty ? "*" : "");
            }
        }

        #region TextContent

        private string _textContent = string.Empty;
        public string TextContent
        {
            get { return _textContent; }
            set
            {
                if (_textContent != value)
                {
                    _textContent = value;
                    OnPropertyChanged("TextContent");
                    IsDirty = true;
                }
            }
        }

        #endregion

        #region IsDirty

        private bool _isDirty = false;
        override public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    OnPropertyChanged("IsDirty");
                    OnPropertyChanged("FileName");
                }
            }
        }

        #endregion

        #region SaveCommand
        RelayCommand<object> _saveCommand = null;
        override public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
//                    _saveCommand = new RelayCommand((p) => OnSave(p), (p) => CanSave(p));
                    _saveCommand = new RelayCommand<object>(OnSave, CanSave);
                }

                return _saveCommand;
            }
        }

        public bool CanSave(object parameter)
        {
            return IsDirty;
        }

        private void OnSave(object parameter)
        {
            MainViewModel.This.Save(this, false);
        }

        #endregion

        #region SaveAsCommand
        RelayCommand<object> _saveAsCommand = null;
        public ICommand SaveAsCommand
        {
            get
            {
                if (_saveAsCommand == null)
                {
                    _saveAsCommand = new RelayCommand<object>(OnSaveAs, CanSaveAs);
                }

                return _saveAsCommand;
            }
        }

        private bool CanSaveAs(object parameter)
        {
            return IsDirty;
        }

        private void OnSaveAs(object parameter)
        {
            MainViewModel.This.Save(this, true);
        }

        #endregion

        #region CloseCommand
        RelayCommand _closeCommand = null;
        override public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(OnClose,  CanClose);
                }

                return _closeCommand;
            }
        }

        private bool CanClose()
        {
            return true;
        }

        private void OnClose()
        {
            MainViewModel.This.Close(this);
        }
        #endregion

        public override Uri IconSource
        {
            get
            {
                // This icon is visible in AvalonDock's Document Navigator window
                return new Uri("pack://application:,,,/RobotTools;component/Images/document.png", UriKind.RelativeOrAbsolute);
            }
        }

        public void SetFileName(string f)
        {
            _filePath = f;
        }
    }
}
