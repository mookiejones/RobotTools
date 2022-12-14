using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using miRobotEditor.Core.Commands;
using miRobotEditor.EditorControl;

namespace miRobotEditor.ViewModels
{
    public class GotoViewModel:ViewModelBase
    {


        #region Properties
        
        #region Editor
        /// <summary>
        /// The <see cref="Editor" /> property's name.
        /// </summary>
        private const string EditorPropertyName = "Editor";

        private Editor _editor = new Editor();  

        /// <summary>
        /// Sets and gets the Editor property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Editor Editor
        {
            get
            {
                return _editor;
            }

            set
            {
                if (_editor == value)
                {
                    return;
                }

                RaisePropertyChanging(EditorPropertyName);
                _editor = value;
                RaisePropertyChanged(EditorPropertyName);
            }
        }
        #endregion
        
        #region Description
        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        private const string DescriptionPropertyName = "Description";

        private string _description = string.Empty;

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                RaisePropertyChanging(DescriptionPropertyName);
                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }
        #endregion

        
        #region EnteredText
        /// <summary>
        /// The <see cref="EnteredText" /> property's name.
        /// </summary>
        private const string EnteredTextPropertyName = "EnteredText";

        private int _enteredText = -1;

        /// <summary>
        /// Sets and gets the EnteredText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int EnteredText
        {
            get
            {
                return _enteredText;
            }

            set
            {
                if (_enteredText == value)
                {
                    return;
                }

                RaisePropertyChanging(EnteredTextPropertyName);
                _enteredText = value;
                RaisePropertyChanged(EnteredTextPropertyName);
            }
        }
        #endregion

        
        #region SelectedLine
        /// <summary>
        /// The <see cref="SelectedLine" /> property's name.
        /// </summary>
        private const string SelectedLinePropertyName = "SelectedLine";

        private int _selectedLine = -1;

        /// <summary>
        /// Sets and gets the SelectedLine property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SelectedLine
        {
            get
            {
                return _selectedLine;
            }

            set
            {
                if (_selectedLine == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedLinePropertyName);
                _selectedLine = value;
                RaisePropertyChanged(SelectedLinePropertyName);
            }
        }
        #endregion
       #endregion

        #region Constructor
        public GotoViewModel(Editor editor)
        {
            Editor = editor;
        }
        public GotoViewModel() { }
        #endregion

        #region Commands
        private RelayCommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand(Accept));
            }
        }
        #endregion

        void Accept()
        {
            var d = Editor.Document.GetLineByNumber(EnteredText);
            Editor.CaretOffset = d.Offset;
            Editor.TextArea.Caret.BringCaretToView();
            Editor.ScrollToLine(_selectedLine);
        }
    }
}
