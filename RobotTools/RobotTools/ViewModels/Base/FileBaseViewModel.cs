using RobotTools.Docking;
using System.Windows.Input;
using RobotTools.UI.Editor;

namespace RobotTools.ViewModels.Base
{

    /// <summary>
    /// Base class that shares common properties, methods, and intefaces
    /// among viewmodels that represent documents in Edi
    /// (text file edits, Start Page, Prgram Settings).
    /// </summary>
    internal abstract class FileBaseViewModel : PaneViewModel,IEditorDocument
    {
       
        #region properties
        /// <summary>
        /// Get/set whether a given file path is a real existing path or not.
        /// 
        /// This is used to identify files that have never been saved and can
        /// those not be remembered in an MRU etc...
        /// </summary>
        public bool IsFilePathReal { get; set; } = false;

        abstract public string FilePath { get; protected set; }

        abstract public bool IsDirty { get; set; }

        #region CloseCommand
        /// <summary>
        /// This command cloases a single file. The binding for this is in the AvalonDock LayoutPanel Style.
        /// </summary>
        abstract public ICommand CloseCommand
        {
            get;
        }

        abstract public ICommand SaveCommand
        {
            get;
        }
        #endregion
        #endregion properties

        private AvalonEditor _textBox;

        public AvalonEditor TextBox
        {
            get => _textBox;
            set
            {
                SetProperty(ref _textBox, value);
                OnPropertyChanged("Title");


            }
        }
    }
}
