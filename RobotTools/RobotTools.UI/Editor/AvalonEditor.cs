using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Search;
using RobotTools.UI.Editor.Background;
using RobotTools.UI.Editor.Bracket;
using RobotTools.UI.Editor.Options;
using RobotTools.UI.Editor.Snippets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using RobotTools.UI.Editor.IconBar;
using RobotTools.UI.Editor.Completion;
using ICSharpCode.AvalonEdit.Document;
using System.IO;

namespace RobotTools.UI.Editor
{
    public partial class AvalonEditor : TextEditor, INotifyPropertyChanged,IDocument


    {
        #region Fields
        private readonly IconBarManager _iconBarManager;
        private readonly IconBarMargin _iconBarMargin;

        public IList<ICompletionDataProvider> CompletionDataProviders { get; set; }
        #endregion

        //static AvalonEditor()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(AvalonEditor), new FrameworkPropertyMetadata(typeof(AvalonEditor)));
        //}
        /// <summary>
        /// Constructor
        /// </summary>
        public AvalonEditor()
        {
            ChangeCommandBindings();
                CompletionDataProviders = new List<ICompletionDataProvider>()
                   {
                      new SnippetCompletionDataProvider()
                 };
                  _iconBarMargin = new IconBarMargin(_iconBarManager = new IconBarManager());
            InitializeEditor();
           //   MouseHoverStopped += (s, e) => _toolTip.IsOpen = false;

            SetHighlighting();

            RegisterEvents();

        }

        event EventHandler<TextChangeEventArgs> IDocument.TextChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        partial void RegisterEvents();
       
      

        private TextEditorOptions _options;

        [Category(CATEGORY)]
        [Description("Text Editor Options")]
        public new TextEditorOptions Options
        {
            get
            {
            return _options ?? (_options = new EditorOptions());
            }
            set
            {
                base.Options = value;
            }
        }

    

        private void ChangeCommandBindings()
        {

            var bindings = TextArea.DefaultInputHandler.Editing.CommandBindings
               .Where(o => o.Command == AvalonEditCommands.DeleteLine);

            foreach(var binding in bindings)
            {
                var keyGesture = new KeyGesture(Key.L, ModifierKeys.Control);
                var command = new RoutedCommand("DeleteLine", typeof(AvalonEditor), new InputGestureCollection { keyGesture });
                binding.Command = command;
            }
            ICollection<CommandBinding> commandBindings = TextArea.DefaultInputHandler.Editing.CommandBindings;


           
        }

        /// <summary>
        /// A bindable Text property
        /// </summary>
        public new string Text
        {
            get { return base.Text; }
            set { 
                base.Text = value;
               // SetValue(TextProperty, value);
                RaisePropertyChanged(nameof(Text));
                HandleTextChanged();
            }
        }


        /// <summary>
        /// The bindable text property dependency property
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AvalonEditor), new PropertyMetadata(HandleTextPropertyChanged)); 

        private static void HandleTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as AvalonEditor;
            target.Text = (string)e.NewValue;
            
        }

        protected override void OnTextChanged(EventArgs e)
        {
           
            base.OnTextChanged(e);
           
            RaisePropertyChanged(nameof(Text));
        }

        /// <summary>
        /// Raises a property changed event
        /// </summary>
        /// <param name="propertyName">The name of the property that updates</param>
        public void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public event PropertyChangedEventHandler PropertyChanged;



        private string _fileName;

        [Category("Editor Properties")]
        [Description("Name of file in editor")]
        public string Filename
        {
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged(nameof(Filename));
            }
        }

        /// <summary>
        /// The bindable text property dependency property
        /// </summary>
        public static readonly DependencyProperty FilenameProperty =
            DependencyProperty.Register("Filename", typeof(string), typeof(AvalonEditor), new PropertyMetadata(HandleFilenamePropertyChanged)
            );

        private static void HandleFilenamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as AvalonEditor;
            target.Filename = (string)e.NewValue;

        }

        protected override bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {

            switch (managerType.Name)
            {
                case "TextChanged":
                    //   FindBookmarkMembers();
                    //IsModified = true;
                    UpdateFolds();
                    break;
            }
            return base.ReceiveWeakEvent(managerType, sender, e);
        }

        /// <summary>
        ///     Raises the <see cref="E:ICSharpCode.AvalonEdit.TextEditor.OptionChanged" /> event.
        /// </summary>
        protected override void OnOptionChanged(PropertyChangedEventArgs e)
        {
            base.OnOptionChanged(e);
            Console.WriteLine(e.PropertyName);
            var propertyName = e.PropertyName;
            if (propertyName != null)
            {
                if (propertyName == "EnableFolding")
                {
                    UpdateFolds();
                }
            }
        }


        private void InitializeEditor()
        {
            //    TextArea.LeftMargins.Insert(0, _iconBarMargin);
            SearchPanel.Install(TextArea);
            TextChanged += ExecuteTextChanged;
            TextArea.TextEntered += TextEntered;
             
            TextArea.Caret.PositionChanged += CaretPositionChanged;
            DataContext = this;
        }

        private void ExecuteTextChanged(object sender, EventArgs e)
        {
            HandleTextChanged();
        }

    

        private void UpdateLineTransformers()
        {
            TextArea.TextView.BackgroundRenderers.Clear();
            var editorOptions = Options as EditorOptions;

            TextArea.TextView.BackgroundRenderers.Add(new BackgroundRenderer(Document.GetLineByOffset(CaretOffset)));

            if (_bracketRenderer == null)
            {
                _bracketRenderer = new BracketHighlightRenderer(TextArea.TextView);
            }
            else
            {
                TextArea.TextView.BackgroundRenderers.Add(_bracketRenderer);
            }
        }

        private void CaretPositionChanged(object sender, EventArgs e)
        {
            var caret = sender as Caret;
            UpdateLineTransformers();
            if (caret != null)
            {
                RaisePropertyChanged("Line");
                RaisePropertyChanged("Column");
                RaisePropertyChanged("Offset");
                //FileSave = ((!string.IsNullOrEmpty(Filename))
                //    ? File.GetLastWriteTime(Filename).ToString(CultureInfo.InvariantCulture)
                //    : string.Empty);
            }
            HighlightBrackets(sender, e);
        }


        private InsightWindow insightWindow;

        private void HandleTextChanged()
        {
            var wordBeforeCaret = this.GetWordBeforeCaret(GetWordParts());

            if (SnippetManager.HasSnippetsFor(wordBeforeCaret, DocumentType))
            {
                insightWindow = new InsightWindow(TextArea)
                {
                    Content = "Press tab to enter snippet",
                    Background = Brushes.Linen
                };
                insightWindow.Show();
                return;
            }

            var text = FindWord();
            if (IsModified || IsModified)
            {
                UpdateFolds();
            }
            if (text == null || !(string.IsNullOrEmpty(text) | text.Length < 3))
            {
                //    ShowCompletionWindow(text);
            }
        }
        private void TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (!IsReadOnly && e.Text.Length == 1)
            {
                var newChar = e.Text[0];
                if (UseCodeCompletion)
                {
                    Complete(newChar);
                }
            }
            //if (CompletionWindow != null)
            //{
            //    return;
            //}

            HandleTextChanged();

        }
        protected internal virtual char[] GetWordParts()
        {
            return new char[0];
        }
        public string DocumentType { get; set; }
        public string FindWord()
        {
            var line = GetLine(TextArea.Caret.Line);
            var text = line;
            var anyOf = new[]
            {
                ' ',
                '=',
                '(',
                ')',
                '[',
                ']',
                '<',
                '>',
                '\r',
                '\n'
            };
            var num = line.IndexOfAny(anyOf, TextArea.Caret.Column - 1);
            if (num > -1)
            {
                text = line.Substring(0, num);
            }
            var num2 = text.LastIndexOfAny(anyOf) + 1;
            if (num2 > -1)
            {
                text = text.Substring(num2).Trim();
            }
            return text;
        }


        private string GetLine(int idx)
        {
            var lineByNumber = Document.GetLineByNumber(idx);
            return Document.GetText(lineByNumber.Offset, lineByNumber.Length);
        }


        private void Reload()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to reload file?", "Reload file",
                MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (messageBoxResult == MessageBoxResult.OK | !IsModified)
            {
                Load(Filename);
                UpdateFolds();
            }
        }

        public event EventHandler IsModifiedChanged;
        public event EventHandler<TextChangeEventArgs> TextChanging;
        public event EventHandler ChangeCompleted;
        public event EventHandler FileNameChanged;

        public void InvokeModifiedChanged(bool isNowModified)
        {
            IsModified = isNowModified;
            if (IsModifiedChanged != null)
                IsModifiedChanged(this, new EventArgs());
        }

        public IDocumentLine GetLineByNumber(int lineNumber) => Document.GetLineByNumber(lineNumber);

        public IDocumentLine GetLineByOffset(int offset) => Document.GetLineByOffset(offset);
       

        public int GetOffset(int line, int column) => Document.GetOffset(Line,column);

        public int GetOffset(TextLocation location) => Document.GetOffset(location);

        public TextLocation GetLocation(int offset) => Document.GetLocation(offset);

        public void Insert(int offset, string text) => Document.Insert(offset, text);

        public void Insert(int offset, ITextSource text) => Document.Insert(Offset, text);

        public void Insert(int offset, string text, AnchorMovementType defaultAnchorMovementType) => Document.Insert(offset, text, defaultAnchorMovementType);

        public void Insert(int offset, ITextSource text, AnchorMovementType defaultAnchorMovementType) => Document.Insert(Offset, text, defaultAnchorMovementType);

        public void Remove(int offset, int length) => Document.Remove(offset, length);

        public void Replace(int offset, int length, string newText) => Document.Replace(offset, length, newText);

        public void Replace(int offset, int length, ITextSource newText) => Document.Replace(offset, length, newText);

        public void StartUndoableAction()
        {
            throw new NotImplementedException();
        }

        public void EndUndoableAction()
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenUndoGroup()
        {
            throw new NotImplementedException();
        }

        public ITextAnchor CreateAnchor(int offset) => Document.CreateAnchor(offset);

        public ITextSource CreateSnapshot() => Document.CreateSnapshot();

        public ITextSource CreateSnapshot(int offset, int length) => Document.CreateSnapshot(offset, length); 

        public TextReader CreateReader() => Document.CreateReader();

        public TextReader CreateReader(int offset, int length) => Document.CreateReader(offset, length); 

        public char GetCharAt(int offset) => Document.GetCharAt(offset); 

        public string GetText(int offset, int length) => Document.GetText(offset, length);


        public string GetText(ISegment segment) => Document.GetText(segment);


        public void WriteTextTo(TextWriter writer) => Document.WriteTextTo(writer);


        public void WriteTextTo(TextWriter writer, int offset, int length) => Document.WriteTextTo(writer, offset, length);


        public int IndexOf(char c, int startIndex, int count) => Document.IndexOf(c, startIndex, count);


        public int IndexOfAny(char[] anyOf, int startIndex, int count) => Document.IndexOfAny(anyOf, startIndex, count);


        public int IndexOf(string searchText, int startIndex, int count, StringComparison comparisonType) => Document.IndexOf(searchText, startIndex, count, comparisonType);

        public int LastIndexOf(char c, int startIndex, int count) => Document.LastIndexOf(c, startIndex, count);

        public int LastIndexOf(string searchText, int startIndex, int count, StringComparison comparisonType)
            => Document.LastIndexOf(searchText, startIndex, count, comparisonType);
 
        [Category(CATEGORY)]
        [Description("Current Line")]
        public int Line=> TextArea.Caret.Column; 
        

        [Category(CATEGORY)]
        [Description("Current Column of Caret")]
        public int Column=>TextArea.Caret.Column;

        [Category(CATEGORY)]
        [Description("Current offset of Caret in editor.")]
        public int Offset=> TextArea.Caret.Offset;

        public string FileName => throw new NotImplementedException();

        public ITextSourceVersion Version => throw new NotImplementedException();

        public int TextLength => throw new NotImplementedException();
    }
   
}
