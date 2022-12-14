using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using RobotTools.Editor.RobotTools.Editor;

namespace RobotTools.Editor.TextEditor
{

    public partial class AvalonEditor
    {
        private const string CATEGORY = "Editor Properties";

        #region ToggleFoldsCommand

        private RelayCommand _toggleFoldsCommand;

        /// <summary>
        ///     Gets the ToggleFoldsCommand.
        /// </summary>
        public RelayCommand ToggleFoldsCommand =>
            _toggleFoldsCommand
            ?? (_toggleFoldsCommand = new RelayCommand(ToggleFolds, CanToggleFolds));

        public bool CanToggleFolds()
        {
            return _foldingManager != null && _foldingManager.AllFoldings.Any();
        }

        #endregion ToggleFoldsCommand

        #region OpenAllFoldsCommand

        private RelayCommand _openAllFoldsCommand;

        /// <summary>
        ///     Gets the OpenAllFoldsCommand.
        /// </summary>
        public RelayCommand OpenAllFoldsCommand =>
            _openAllFoldsCommand
            ?? (_openAllFoldsCommand = new RelayCommand(ExecuteOpenAllFoldsCommand, CanChangeFoldStatus));

        private void ChangeFoldStatus(bool isFolded)
        {
            foreach (var current in _foldingManager.AllFoldings)
            {
                current.IsFolded = isFolded;
            }
        }

        private void ExecuteOpenAllFoldsCommand()
        {
            ChangeFoldStatus(false);
        }

        private bool CanChangeFoldStatus()
        {
            return _foldingManager != null && _foldingManager.AllFoldings.Any();
        }

        #endregion OpenAllFoldsCommand

        #region ToggleAllFoldsCommand

        private ICommand _toggleAllFoldsCommand;

        /// <summary>
        ///     Gets the ToggleAllFoldsCommand.
        /// </summary>
        public ICommand ToggleAllFoldsCommand =>
            _toggleAllFoldsCommand
            ?? (_toggleAllFoldsCommand = new RelayCommand(ToggleAllFolds, CanToggleFolds));

        #endregion ToggleAllFoldsCommand

        #region CloseAllFoldsCommand

        private ICommand _closeAllFoldsCommand;

        /// <summary>
        ///     Gets the CloseAllFoldsCommand.
        /// </summary>
        [Category(CATEGORY)]
        [Description("Close all folds")]
        public ICommand CloseAllFoldsCommand =>
            _closeAllFoldsCommand ?? (_closeAllFoldsCommand = new RelayCommand(
                ExecuteCloseAllFoldsCommand,
                CanExecuteCloseAllFoldsCommand));

        private void ExecuteCloseAllFoldsCommand()
        {
            ChangeFoldStatus(true);
        }

        private bool CanExecuteCloseAllFoldsCommand()
        {
            return _foldingManager != null && _foldingManager.AllFoldings.Any();
        }

        #endregion CloseAllFoldsCommand

     
        #region UndoCommand
        private RelayCommand _undoCommand;
        public RelayCommand UndoCommand => _undoCommand ?? (_undoCommand = new RelayCommand(ExecuteUndoCommand, CanExecuteUndoCommand));

        private void ExecuteUndoCommand()
        {
            Undo();
        }

        private bool CanExecuteUndoCommand() => true;


        #endregion

        #region RedoCommand
        private RelayCommand _RedoCommand;
        public RelayCommand RedoCommand => _RedoCommand ?? (_RedoCommand = new RelayCommand(ExecuteRedoCommand, CanExecuteRedoCommand));

        private void ExecuteRedoCommand()
        {
            Redo();
        }

        private bool CanExecuteRedoCommand()
        {
            return true;
        }
        #endregion

        #region CutCommand
        private RelayCommand _CutCommand;
        public RelayCommand CutCommand => _CutCommand ?? (_CutCommand = new RelayCommand(ExecuteCutCommand, CanExecuteCutCommand));

        private void ExecuteCutCommand() => Cut();

        private bool CanExecuteCutCommand() => true;
        #endregion

        #region CopyCommand
        private RelayCommand _CopyCommand;
        public RelayCommand CopyCommand => _CopyCommand ?? (_CopyCommand = new RelayCommand(ExecuteCopyCommand, CanExecuteCopyCommand));

        private void ExecuteCopyCommand() => Copy();


        private bool CanExecuteCopyCommand() => true;

        #endregion

        #region PasteCommand
        private RelayCommand _PasteCommand;
        public RelayCommand PasteCommand => _PasteCommand ?? (_PasteCommand = new RelayCommand(ExecutePasteCommand, CanExecutePasteCommand));

        private void ExecutePasteCommand() => Paste();


        private bool CanExecutePasteCommand() => true;

        #endregion
    }
}
