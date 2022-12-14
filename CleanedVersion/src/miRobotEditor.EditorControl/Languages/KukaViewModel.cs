using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core.Interfaces;
using miRobotEditor.EditorControl.Interfaces;
using miRobotEditor.UI.Controls;

namespace miRobotEditor.EditorControl.Languages
{
    public class KukaViewModel : DocumentModel, IDocument
    {

        public KukaViewModel(string filepath,AbstractLanguageClass lang): base(filepath,lang)
        {

            Grid.Loaded += Grid_Loaded;

            ShowGrid = false;
            FileLanguage = lang;
            Source.FileLanguage = FileLanguage;
            Data.FileLanguage = FileLanguage;
            Source.GotFocus += (s, e) => { TextBox = s as Editor; };
            Data.GotFocus += (s, e) => { TextBox = s as Editor; };
            Source.TextChanged += (s, e) => TextChanged(s);
            Data.TextChanged += (s, e) => TextChanged(s);
            Source.IsModified = false;
            Data.IsModified = false;

        }

        void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
            Grid.IsAnimated = false;
            Grid.Collapse();
            Grid.IsCollapsed = true;
            Grid.IsAnimated = true;
        }
       
        #region Public Events

        //    public event UpdateFunctionEventHandler TextUpdated;

        #endregion

        #region Commands
        private RelayCommand _toggleGridCommand;

        public ICommand ToggleGridCommand
        {
            get { return _toggleGridCommand ?? (_toggleGridCommand = new RelayCommand(ToggleGrid, () => Grid != null)); }
        }
        private RelayCommand _closeCommand;
        public new ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(CloseWindow)); }
        }

        #endregion



        #region Properties




        private ExtendedGridSplitter _grid = new ExtendedGridSplitter();
        public ExtendedGridSplitter Grid { get { return _grid; } set { _grid = value; RaisePropertyChanged("Grid"); } }

        
        #region DataRow
        /// <summary>
        /// The <see cref="DataRow" /> property's name.
        /// </summary>
        public const string DataRowPropertyName = "DataRow";

        private int _dataRow = 2;

        /// <summary>
        /// Sets and gets the DataRow property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int DataRow
        {
            get
            {
                return _dataRow;
            }

            set
            {
                if (_dataRow == value)
                {
                    return;
                }

                RaisePropertyChanging(DataRowPropertyName);
                _dataRow = value;
                RaisePropertyChanged(DataRowPropertyName);
            }
        }
        #endregion
        
        #region GridRow
        /// <summary>
        /// The <see cref="GridRow" /> property's name.
        /// </summary>
        public const string GridRowPropertyName = "GridRow";

        private int _gridRow = 1;

        /// <summary>
        /// Sets and gets the GridRow property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int GridRow
        {
            get
            {
                return _gridRow;
            }

            set
            {
                if (_gridRow == value)
                {
                    return;
                }

                RaisePropertyChanging(GridRowPropertyName);
                _gridRow = value;
                RaisePropertyChanged(GridRowPropertyName);
            }
        }
        #endregion
        
        #region Source
        /// <summary>
        /// The <see cref="Source" /> property's name.
        /// </summary>
        public const string SourcePropertyName = "Source";

        private Editor _source = new Editor();

        /// <summary>
        /// Sets and gets the Source property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Editor Source
        {
            get
            {
                return _source;
            }

            set
            {
                if (_source == value)
                {
                    return;
                }

                RaisePropertyChanging(SourcePropertyName);
                _source = value;
                RaisePropertyChanged(SourcePropertyName);
            }
        }
        #endregion

        #region Data
        /// <summary>
        /// The <see cref="Data" /> property's name.
        /// </summary>
        public const string DataPropertyName = "Data";

        private Editor _data = new Editor();

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Editor Data
        {
            get
            {
                return _data;
            }

            set
            {
                if (_data == value)
                {
                    return;
                }

                RaisePropertyChanging(DataPropertyName);
                _data = value;
                RaisePropertyChanged(DataPropertyName);
            }
        }
        #endregion
   
        #endregion

       
        public new void CloseWindow()
        {
            CheckClose(Data);
            CheckClose(Source);



            //TODO reimplement this
//          throw new NotImplementedException();
//            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
//           main.Close(this);
        }

        /// <summary>
        /// Checks both boxes to determine if they should be saved or not
        /// </summary>
        /// <param name="txtBox"></param>
        void CheckClose(Editor txtBox)
        {
            if (txtBox != null)
                if (txtBox.IsModified)
                {
                    var res = MessageBox.Show(string.Format("Save changes for file '{0}'?", txtBox.Filename), "miRobotEditor", MessageBoxButton.YesNoCancel);
                    if (res == MessageBoxResult.Cancel)
                        return;
                    if (res == MessageBoxResult.Yes)
                    {
                        Save(txtBox);
                    }
                }
        }
        private bool ShowGrid
        {
            set
            {
                switch (value)
                {
                    case true:
                        Data.Text = FileLanguage.DataText;
// ReSharper disable AssignNullToNotNullAttribute

                        var dn = Path.GetDirectoryName(FilePath);
                        Data.Filename = Path.Combine(dn, FileLanguage.DataName);
// ReSharper restore AssignNullToNotNullAttribute
                        Data.SetHighlighting();
                        Data.Visibility = Visibility.Visible;
                        Grid.Visibility = Visibility.Visible;
                        GridRow = 1;
                        DataRow = 2;
                        break;
                    case false:
                        if (Data == null)
                            Data = new Editor();
                        Data.Visibility = Visibility.Collapsed;
                        Grid.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        /// <summary>
        /// Select Text from variable offset
        /// </summary>
        /// <remarks>
        /// Selects appropriate editor that text resides in.
        /// </remarks>
        public new  void SelectText(IVariable var)
        {
            if (var.Name == null) throw new ArgumentNullException("var");

            //TODO Need to find out if this will work from Global Variables. Only Tested so far for Local Variable Window

            // Does Textbox have Variables
            if (TextBox.Variables == null)
                SwitchTextBox();


            // Is Offset of textbox greater than desired value?
            var enoughlines = TextBox.Text.Length >= var.Offset;
            if (enoughlines)
                TextBox.SelectText(var);
            else
            {
                TextBox = Data;
                enoughlines = TextBox.Text.Length >= var.Offset;
                if (enoughlines)
                    TextBox.SelectText(var);
            }
        }


        void SwitchTextBox()
        {
            switch (TextBox.EditorType)
            {
                case EDITORTYPE.SOURCE:
                    TextBox = Data;
                    break;
                case EDITORTYPE.DATA:
                    TextBox = Source;
                    break;
            }

        }


        public new void Load(string filepath)
        {
            FilePath = filepath;
            Instance = this;
            TextBox.FileLanguage = FileLanguage;
            Source.FileLanguage = FileLanguage;
            Grid.IsAnimated = false;

            var loadDatFileOnly = Path.GetExtension(filepath) == ".dat";
            //TODO Set Icon For File

            IconSource = Utilities.LoadBitmap(Global.ImgSrc);
            Source.Filename = filepath;
            Source.SetHighlighting();
            Source.Text = loadDatFileOnly ? FileLanguage.DataText : FileLanguage.SourceText;

            if ((FileLanguage is KUKA) && (!String.IsNullOrEmpty(FileLanguage.DataText)) && (Source.Text != FileLanguage.DataText))
            {
                ShowGrid = true;
                Data.FileLanguage = FileLanguage;
// ReSharper disable AssignNullToNotNullAttribute
                Data.Filename = Path.Combine(Path.GetDirectoryName(filepath), FileLanguage.DataName);
// ReSharper restore AssignNullToNotNullAttribute
                Data.Text = FileLanguage.DataText;
                Data.SetHighlighting();
            }
           

            // Select Original File            
            TextBox = Source.Filename == filepath ? Source : Data;
            Grid.IsAnimated = true;
            RaisePropertyChanged("Title");
        }

        void ToggleGrid()
        {

            if (Grid.IsCollapsed)

                Grid.Expand();
            else
                Grid.Collapse();

        }
    }
}