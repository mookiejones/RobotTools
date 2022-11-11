using System;

namespace RobotTools.ViewModels
{
    using CommunityToolkit.Mvvm.Input;
    using RobotTools.ViewModels.MRU;
    using System.Windows.Input;

    class StartPageViewModel : Base.FileBaseViewModel
    {
        public StartPageViewModel()
        {
            Title = "Start Page";
            StartPageTip = "Welcome to Edi. Review the content of the start page to get started.";
            ContentId = "{StartPage_ContentId}";
        }

        #region CloseCommand
        RelayCommand _closeCommand = null;
        override public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(OnClose, CanClose);
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

        override public ICommand SaveCommand
        {
            get
            {
                return null;
            }
        }

        public override Uri IconSource
        {
            get
            {
                // This icon is visible in AvalonDock's Document Navigator window
                return new Uri("pack://application:,,,/RobotTools;component/Images/document.png", UriKind.RelativeOrAbsolute);
            }
        }

        public MRUListVM MruList
        {
            get
            {
                return MainViewModel.This.RecentFiles.MruList;
            }
        }

        public string StartPageTip { get; set; }

        override public bool IsDirty
        {
            get
            {
                return false;
            }

            set
            {
                throw new NotSupportedException("Start page cannot be saved therfore setting dirty cannot be useful.");
            }
        }

        override public string FilePath
        {
            get
            {
                return ContentId;
            }

            protected set
            {
                throw new NotSupportedException();
            }
        }
    }
}
