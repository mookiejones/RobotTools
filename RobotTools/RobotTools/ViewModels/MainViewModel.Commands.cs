using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace RobotTools.ViewModels
{
    partial class MainViewModel
    {
        #region CloseCommand

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new RelayCommand(ExecuteCloseCommand, CanExecuteDocument));

        private void ExecuteCloseCommand()
        {
            throw new NotImplementedException();
        }
 

        #endregion

        #region SearchCommand

        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new RelayCommand(ExecuteSearch, CanExecuteDocument));

        private void ExecuteSearch()
        {
            throw new NotImplementedException();
        }



        #endregion

        #region FindCommand

        private RelayCommand _findCommand;

        public RelayCommand FindCommand =>
            _findCommand ?? (_findCommand = new RelayCommand(ExecuteFind, CanExecuteDocument));

        private void ExecuteFind()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region FindReplaceCommand

        private RelayCommand _findReplaceCommand;

        public RelayCommand FindReplaceCommand => _findReplaceCommand ??
                                                  (_findReplaceCommand = new RelayCommand(ExecuteFindReplace,
                                                      CanExecuteDocument));

        private void ExecuteFindReplace()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ImportCommand

        private RelayCommand _importCommand;

        public RelayCommand ImportCommand =>
            _importCommand ?? (_importCommand = new RelayCommand(ExecuteImport, CanExecuteDocument));

        private void ExecuteImport()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ReloadCommand
        private RelayCommand _reloadCommand;
        public RelayCommand ReloadCommand => _reloadCommand ?? (_reloadCommand = new RelayCommand(ExecuteReloadCommand, CanExecuteReloadCommand));

        private void ExecuteReloadCommand()
        {
        }

        private bool CanExecuteReloadCommand()
        {
            return true;
        }
        #endregion

        #region SaveAsCommand
        private RelayCommand _saveAsCommand;
        public RelayCommand SaveAsCommand => _saveAsCommand ?? (_saveAsCommand = new RelayCommand(ExecuteSaveAsCommand, CanExecuteSaveAsCommand));

        private void ExecuteSaveAsCommand()
        {
        }

        private bool CanExecuteSaveAsCommand()
        {
            return true;
        }
        #endregion

        #region ExitCommand
        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(ExecuteExitCommand, CanExecuteExitCommand));

        private void ExecuteExitCommand()
        {
        }

        private bool CanExecuteExitCommand()
        {
            return true;
        }
        #endregion

        #region ToggleCommentCommand
        private RelayCommand _toggleCommentCommand;
        public RelayCommand ToggleCommentCommand => _toggleCommentCommand ?? (_toggleCommentCommand = new RelayCommand(ExecuteToggleCommentCommand, CanExecuteToggleCommentCommand));

        private void ExecuteToggleCommentCommand()
        {
        }

        private bool CanExecuteToggleCommentCommand()
        {
            return true;
        }
        #endregion

        #region IncreaseLineIndentCommand
        private RelayCommand _increaseLineIndentCommand;
        public RelayCommand IncreaseLineIndentCommand => _increaseLineIndentCommand ?? (_increaseLineIndentCommand = new RelayCommand(ExecuteIncreaseLineIndentCommand, CanExecuteIncreaseLineIndentCommand));

        private void ExecuteIncreaseLineIndentCommand()
        {
        }

        private bool CanExecuteIncreaseLineIndentCommand()
        {
            return true;
        }
        #endregion

        #region DecreaseLineIndentCommand
        private RelayCommand _decreaseLineIndentCommand;
        public RelayCommand DecreaseLineIndentCommand => _decreaseLineIndentCommand ?? (_decreaseLineIndentCommand = new RelayCommand(ExecuteDecreaseLineIndentCommand, CanExecuteDecreaseLineIndentCommand));

        private void ExecuteDecreaseLineIndentCommand()
        {
        }

        private bool CanExecuteDecreaseLineIndentCommand() => true;
        #endregion
        #region ShowFindReplaceCommand
        private RelayCommand _showFindReplaceCommand;
        public RelayCommand ShowFindReplaceCommand => _showFindReplaceCommand ?? (_showFindReplaceCommand = new RelayCommand(ExecuteShowFindReplaceCommand, CanExecuteShowFindReplaceCommand));

        private void ExecuteShowFindReplaceCommand()
        {
        }

        private bool CanExecuteShowFindReplaceCommand() => true;
        #endregion 
        private bool CanExecuteDocument() => ActiveDocument != null;

    }
}
