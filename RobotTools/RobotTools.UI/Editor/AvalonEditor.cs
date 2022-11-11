using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using RobotTools.UI.Editor.Bracket;
using RobotTools.UI.Editor.Completion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RobotTools.UI.Editor
{
    public partial class AvalonEditor: TextEditor,INotifyPropertyChanged
    {
        #region Constants

        /*
                private const int LogicListFontSizeMax = 50;
                private const int LogicListFontSizeMin = 10;
        */
        private const double Epsilon = 1E-08;

        #endregion

        #region Dependency Properties

        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                RaisePropertyChanged("Text");
            }
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Members

     
        private readonly MyBracketSearcher _bracketSearcher = new MyBracketSearcher();

        private readonly IconBarManager _iconBarManager;
        private readonly IconBarMargin _iconBarMargin;
        private readonly ReadOnlyObservableCollection<IVariable> _readonlyVariables = null;
        private readonly ObservableCollection<IVariable> _variables = new ObservableCollection<IVariable>();
        private BracketHighlightRenderer _bracketRenderer;
        private string _fileSave = string.Empty;
        private string _filename = string.Empty;

        private KeyEventArgs _lastKeyUpArgs;
        private ToolTip _toolTip = new ToolTip();
        private static BitmapImage _imgMethod;
        // ReSharper disable once UnassignedField.Compiler
        private static readonly BitmapImage _imgStruct = Utilities.LoadBitmap(Global.ImgStruct);
        private static BitmapImage _imgEnum;
        private static BitmapImage _imgSignal;
        // ReSharper disable once UnassignedField.Compiler
        private static BitmapImage _imgField;
        private static BitmapImage _imgXyz;

        private AbstractLanguageClass _filelanguage;

        #endregion

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
            typeof(AvalonEditor), new PropertyMetadata((obj, args) =>
            {
                var target = (AvalonEditor)obj;
                target.Text = (string)args.NewValue;
            }));

        private void ChangeCommandBindings()
        {
            ICollection<CommandBinding> commandBindings = base.TextArea.DefaultInputHandler.Editing.CommandBindings;
            foreach (CommandBinding current in commandBindings)
            {
                if (current.Command == AvalonEditCommands.DeleteLine)
                {
                    RoutedCommand command = new RoutedCommand("DeleteLine", typeof(Editor), new InputGestureCollection
            {
                new KeyGesture(Key.L, ModifierKeys.Control)
            });
                    current.Command = command;
                    break;
                }
            }
        }

        public AvalonEditor()
        {
            try
            {
                ChangeCommandBindings();
               
                CompletionDataProviders = new List<ICompletionDataProvider>()
                {
                    new SnippetCompletionDataProvider()
                };
                _iconBarMargin = new IconBarMargin(_iconBarManager = new IconBarManager());
                Initialize();
                InitializeMyControl();
                MouseHoverStopped += (s, e) => _toolTip.IsOpen = false;
            }
            finally
            {
                this.InvokeModifiedChanged(false);
            }
        }
        public event EventHandler IsModifiedChanged;
        public void InvokeModifiedChanged(bool isNowModified)
        {
            IsModified = isNowModified;
            if (IsModifiedChanged != null)
                IsModifiedChanged(this, new EventArgs());

        }

     
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        public int Line
        {
            get { return TextArea.Caret.Column; }
        }

        public int Column
        {
            get { return TextArea.Caret.Column; }
        }

        public int Offset
        {
            get { return TextArea.Caret.Offset; }
        }

        public string FileSave
        {
            get { return _fileSave; }
            set
            {
                _fileSave = value;
                OnPropertyChanged("FileSave");
            }
        }

      

    

        #region Filename

        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                OnPropertyChanged("Filename");
                OnPropertyChanged("Title");
            }
        }

        #endregion





        #endregion

        partial void Initialize();
     

        private void InitializeMyControl()
        {
            TextArea.LeftMargins.Insert(0, _iconBarMargin);
            SearchPanel.Install(TextArea);
            TextArea.TextEntered += TextEntered;
            TextArea.Caret.PositionChanged += CaretPositionChanged;
            DataContext = this;
        }

        /// <summary>
        ///     HighlightBrackets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
// ReSharper disable UnusedParameter.Local
        private void HighlightBrackets(object sender, EventArgs e)
        {
            var highlight = _bracketSearcher.SearchBracket(Document, TextArea.Caret.Offset);
            _bracketRenderer.SetHighlight(highlight);
        }

        // ReSharper restore UnusedParameter.Local

        private void CaretPositionChanged(object sender, EventArgs e)
        {
            var caret = sender as Caret;
            UpdateLineTransformers();
            if (caret != null)
            {
                OnPropertyChanged("Line");
                OnPropertyChanged("Column");
                OnPropertyChanged("Offset");
                FileSave = ((!string.IsNullOrEmpty(Filename))
                    ? File.GetLastWriteTime(Filename).ToString(CultureInfo.InvariantCulture)
                    : string.Empty);
            }
            HighlightBrackets(sender, e);
        }

        private void UpdateLineTransformers()
        {
            TextArea.TextView.BackgroundRenderers.Clear();
            var editorOptions = Options as EditorOptions;
            if (editorOptions != null && editorOptions.HighlightCurrentLine)
            {
                TextArea.TextView.BackgroundRenderers.Add(new BackgroundRenderer(Document.GetLineByOffset(CaretOffset)));
            }
            if (_bracketRenderer == null)
            {
                _bracketRenderer = new BracketHighlightRenderer(TextArea.TextView);
            }
            else
            {
                TextArea.TextView.BackgroundRenderers.Add(_bracketRenderer);
            }
        }

        /// <summary>
        ///     Invoked when an unhandled <see cref="E:System.Windows.Input.Keyboard.KeyUp" /> attached event reaches an element in
        ///     its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_lastKeyUpArgs == null)
            {
                _lastKeyUpArgs = e;
            }
            else
            {
                var modifiers = Keyboard.Modifiers;
                if (modifiers == ModifierKeys.Control)
                {
                    var key = e.Key;
                    if (key == Key.O)
                    {
                        if (!string.IsNullOrEmpty(FileLanguage.CommentChar))
                        {
                            ToggleFolds();
                        }
                    }
                }
                _lastKeyUpArgs = e;
                base.OnKeyUp(e);
            }
        }

   


        

     
     

        /// <inheritdoc cref="M:System.Windows.IWeakEventListener.ReceiveWeakEvent(System.Type,System.Object,System.EventArgs)" />
        /// //TODO Reimplement this
        protected override bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            switch (managerType.Name)
            {
                case "TextChanged":
                    FindBookmarkMembers();
                    //IsModified = true;
                    UpdateFolds();
                    break;
            }
            return base.ReceiveWeakEvent(managerType, sender, e);
        }

        private void ChangeIndent(object param)
        {
            try
            {
                var flag = Convert.ToBoolean(param);
                var lineByOffset = Document.GetLineByOffset(SelectionStart);
                var lineByOffset2 = Document.GetLineByOffset(SelectionStart + SelectionLength);
                var num = 0;
                using (Document.RunUpdate())
                {
                    var documentLine = lineByOffset;
                    while (documentLine.LineNumber < lineByOffset2.LineNumber + 1)
                    {
                        var line = GetLine(documentLine.LineNumber);
                        var regex = new Regex("(^[\\s]+)");
                        var match = regex.Match(line);
                        if (match.Success)
                        {
                            num = match.Groups[1].Length;
                        }
                        if (flag)
                        {
                            Document.Insert(documentLine.Offset + num, " ");
                        }
                        else
                        {
                            num = ((num > 1) ? (num - 1) : num);
                            if (num >= 1)
                            {
                                Document.Replace(documentLine.Offset, line.Length, line.Substring(1));
                            }
                        }
                        documentLine = documentLine.NextLine;
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = new ErrorMessage("Editor.ChangeIndent", ex);
                Messenger.Default.Send<IMessage>(msg);
            }
        }

        private void ToggleComment()
        {
            if (FileLanguage != null)
            {
                var lineByOffset = Document.GetLineByOffset(SelectionStart);
                var lineByOffset2 = Document.GetLineByOffset(SelectionStart + SelectionLength);
                using (Document.RunUpdate())
                {
                    var documentLine = lineByOffset;
                    while (documentLine.LineNumber < lineByOffset2.LineNumber + 1)
                    {
                        var line = GetLine(documentLine.LineNumber);
                        if (FileLanguage.IsLineCommented(line))
                        {
                            Document.Insert(FileLanguage.CommentOffset(line) + documentLine.Offset,
                                FileLanguage.CommentChar);
                        }
                        else
                        {
                            var text = FileLanguage.CommentReplaceString(line);
                            Document.Replace(documentLine.Offset, line.Length, text);
                        }
                        documentLine = documentLine.NextLine;
                    }
                }
            }
        }

        private bool CanSave()
        {
            return File.Exists(Filename) ? IsModified : IsModified;
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

        public void SetHighlighting()
        {
            try
            {
                if (Filename != null)
                {
                    SyntaxHighlighting =
                        HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(Filename));
                }
            }
            catch (Exception ex)
            {
                //var msg = new ErrorMessage(string.Format("Could not load Syntax Highlighting for {0}", Filename), ex,
                //    MessageType.Error);
                //Messenger.Default.Send<IMessage>(msg);
            }
        }

        private void Complete(char newChar)
        {

        }
        public string DocumentType { get; set; }
        public bool UseCodeCompletion { get; set; }
        private void TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (!base.IsReadOnly && e.Text.Length == 1)
            {
                var newChar = e.Text[0];
                if (UseCodeCompletion)
                {
                    Complete(newChar);
                }
            }
            if (CompletionWindow != null)
            {
                return;
            }

            string wordBeforeCaret = this.GetWordBeforeCaret(this.GetWordParts());

            if (SnippetManager.HasSnippetsFor(wordBeforeCaret, this.DocumentType))
            {
                insightWindow = new InsightWindow(base.TextArea)
                {
                    Content = "Press tab to enter snippet",
                    Background = Brushes.Linen
                };
                this.insightWindow.Show();
                return;
            }
            if (FileLanguage != null && !(FileLanguage is LanguageBase))
            {
                var text = FindWord();
                if (IsModified || IsModified)
                {
                    UpdateFolds();
                }
                if (text == null || !(string.IsNullOrEmpty(text) | text.Length < 3))
                {
                    ShowCompletionWindow(text);
                }
            }
        }


        private InsightWindow insightWindow;
        private void ShowCompletionWindow(string currentword)
        {
            CompletionWindow = new CompletionWindow(TextArea);
            var completionItems = GetCompletionItems();
            foreach (var current in completionItems)
            {
                CompletionWindow.CompletionList.CompletionData.Add(current);
            }
            CompletionWindow.Closed += delegate { CompletionWindow = null; };
            CompletionWindow.CloseWhenCaretAtBeginning = true;
            CompletionWindow.CompletionList.SelectItem(currentword);
            if (CompletionWindow.CompletionList.SelectedItem != null)
            {
                CompletionWindow.Show();
            }
        }

        private IEnumerable<ICompletionData> GetCompletionItems()
        {
            var list = new List<ICompletionData>();
            list.AddRange(HighlightList());
            list.AddRange(ObjectBrowserCompletionList());
            return list.ToArray();
        }

        private IEnumerable<ICompletionData> HighlightList()
        {
            var items = new List<CodeCompletion>();
            foreach (var current in
                from rule in SyntaxHighlighting.MainRuleSet.Rules
                select rule.Regex.ToString()
                into parseString
                let start = parseString.IndexOf(">", StringComparison.Ordinal) + 1
                let end = parseString.LastIndexOf(")", StringComparison.Ordinal)
                select parseString.Substring(start, end - start)
                into parseString1
                select parseString1.Split(new[]
                {
                    '|'
                })
                into spl
                from item in
                from t in spl
                where !string.IsNullOrEmpty(t)
                select new CodeCompletion(t.Replace("\\b", ""))
                    into item
                where !items.Contains(item) && char.IsLetter(item.Text, 0)
                select item
                select item)
            {
                items.Add(current);
            }
            return items.ToArray();
        }

        private IEnumerable<ICompletionData> ObjectBrowserCompletionList()
        {
            return (
                from v in FileLanguage.Fields
                where v.Type != "def" && v.Type != "deffct"
                select new CodeCompletion(v.Name)
                {
                    Image = v.Icon
                }).ToArray<ICompletionData>();
        }

        private void TextEditor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (CompletionWindow != null)
            {
                if (e.Key == Key.Tab)
                {
                    CompletionWindow.CompletionList.RequestInsertion(e);
                }
                if (e.Key == Key.Return)
                {
                    CompletionWindow = null;
                }
            }
        }

        public void ReplaceAll()
        {
            var regexPattern = FindReplaceViewModel.Instance.RegexPattern;
            var match = regexPattern.Match(Text);
            while (match.Success)
            {
                Document.GetLineByOffset(match.Index);
                regexPattern.Replace(FindReplaceViewModel.Instance.LookFor, FindReplaceViewModel.Instance.ReplaceWith,
                    match.Index);
                match = match.NextMatch();
            }
        }

        public void ReplaceText()
        {
            FindText();
            SelectedText = SelectedText.Replace(SelectedText, FindReplaceViewModel.Instance.ReplaceWith);
        }

        public void FindText()
        {
            var num = Text.IndexOf(FindReplaceViewModel.Instance.LookFor, CaretOffset, StringComparison.Ordinal);
            if (num > -1)
            {
                Document.GetLineByOffset(num);
                JumpTo(new Variable
                {
                    Offset = num
                });
                SelectionStart = num;
                SelectionLength = FindReplaceViewModel.Instance.LookFor.Length;
            }
            else
            {
                FindReplaceViewModel.Instance.SearchResult = "No Results Found, Starting Search from Beginning";
                CaretOffset = 0;
            }
        }

        private void JumpTo(IVariable i)
        {
            var location = Document.GetLocation(Convert.ToInt32(i.Offset));
            ScrollTo(location.Line, location.Column);
            SelectionStart = Convert.ToInt32(i.Offset);
            SelectionLength = i.Value.Length;
            Focus();
            if (EditorOptions.Instance.EnableAnimations)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(DisplayCaretHighlightAnimation));
            }
        }

        private void DisplayCaretHighlightAnimation()
        {
            if (TextArea != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(TextArea.TextView);
                if (adornerLayer != null)
                {
                    var adorner = new CaretHighlightAdorner(TextArea);
                    adornerLayer.Add(adorner);
                }
            }
        }

        public void SelectText(IVariable var)
        {
            if (var != null)
            {
                if (var.Name == null)
                {
                    throw new ArgumentNullException("var");
                }
                var lineByOffset = Document.GetLineByOffset(var.Offset);
                TextArea.Caret.BringCaretToView();
                CaretOffset = lineByOffset.Offset;
                ScrollToLine(lineByOffset.LineNumber);
                var foldingsAt = _foldingManager.GetFoldingsAt(lineByOffset.Offset);
                if (foldingsAt.Count > 0)
                {
                    var foldingSection = foldingsAt[0];
                    foldingSection.IsFolded = false;
                }
                FindText(var.Offset, var.Name);
                JumpTo(var);
            }
        }

        private void FindText(int startOffset, string text)
        {
            var selectionStart = Text.IndexOf(text, startOffset, StringComparison.OrdinalIgnoreCase);
            SelectionStart = selectionStart;
            SelectionLength = text.Length;
        }

        public void FindText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            SelectionStart = Text.IndexOf(text, CaretOffset, StringComparison.Ordinal);
        }

        public void ShowFindDialog()
        {
            FindAndReplaceWindow.Instance.ShowDialog();
        }

        private void EditorPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var editorOptions = Options as EditorOptions;
            if (editorOptions == null || editorOptions.MouseWheelZoom)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    if (e.Delta <= 0 || FontSize >= 50.0)
                    {
                        FontSize -= 1.0;
                    }
                    else
                    {
                        if (e.Delta > 0 && FontSize > 10.0)
                        {
                            FontSize += 1.0;
                        }
                    }
                    e.Handled = true;
                }
            }
        }

        [Localizable(false)]
        private void UpdateFolds()
        {
            var editorOptions = Options as EditorOptions;
            var flag = editorOptions != null && editorOptions.EnableFolding;
            if (SyntaxHighlighting == null)
            {
                _foldingStrategy = null;
            }
            if (File.Exists(Filename))
            {
                if (Path.GetExtension(Filename) == ".xml" || Path.GetExtension(Filename) == ".cfg")
                {
                    _foldingStrategy = new XmlFoldingStrategy();
                }
                else
                {
                    if (FileLanguage != null)
                    {
                        _foldingStrategy = FileLanguage.FoldingStrategy;
                    }
                }
                if (_foldingStrategy != null && flag)
                {
                    if (_foldingManager == null)
                    {
                        _foldingManager = FoldingManager.Install(TextArea);
                    }

                    var xmlStrategy = _foldingStrategy as XmlFoldingStrategy;
                    if (xmlStrategy != null)
                    {
                        xmlStrategy.UpdateFoldings(_foldingManager, Document);
                    }
                    else
                    {
                        ((AbstractFoldingStrategy)_foldingStrategy).UpdateFoldings(_foldingManager, Document);

                    }

                    RegisterFoldTitles();
                }
                else
                {
                    if (_foldingManager != null)
                    {
                        FoldingManager.Uninstall(_foldingManager);
                        _foldingManager = null;
                    }
                }
            }
        }

        private void RegisterFoldTitles()
        {
            if (!(DocumentViewModel.Instance.FileLanguage is LanguageBase) || !(Path.GetExtension(Filename) == ".xml"))
            {
                foreach (var current in _foldingManager.AllFoldings)
                {
                    current.Title = DocumentViewModel.Instance.FileLanguage.FoldTitle(current, Document);
                }
            }
        }

        private string GetLine(int idx)
        {
            var lineByNumber = Document.GetLineByNumber(idx);
            return Document.GetText(lineByNumber.Offset, lineByNumber.Length);
        }

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

        private bool GetCurrentFold(TextViewPosition loc)
        {
            var offset = Document.GetOffset(loc.Location);
            var foldingsAt = _foldingManager.GetFoldingsAt(offset);
            bool result;
            if (foldingsAt.Count == 0)
            {
                result = false;
            }
            else
            {
                _toolTip = new ToolTip
                {
                    Style = (Style)FindResource("FoldToolTipStyle"),
                    DataContext = foldingsAt,
                    PlacementTarget = this,
                    IsOpen = true
                };
                result = true;
            }
            return result;
        }

        private void Mouse_OnHover(object sender, MouseEventArgs e)
        {
            if (_foldingManager != null)
            {
                var positionFromPoint = GetPositionFromPoint(e.GetPosition(this));
                if (positionFromPoint.HasValue)
                {
                    e.Handled = GetCurrentFold(positionFromPoint.Value);
                }
            }
        }

        private void ToggleFolds()
        {
            if (_foldingManager != null)
            {
                var foldingSection =
                    _foldingManager.GetNextFolding(TextArea.Document.GetOffset(TextArea.Caret.Line,
                        TextArea.Caret.Column));
                if (foldingSection == null ||
                    Document.GetLineByOffset(foldingSection.StartOffset).LineNumber != TextArea.Caret.Line)
                {
                    foldingSection = _foldingManager.GetFoldingsContaining(TextArea.Caret.Offset).LastOrDefault();
                }
                if (foldingSection != null)
                {
                    foldingSection.IsFolded = !foldingSection.IsFolded;
                }
            }
        }

        private void ToggleAllFolds()
        {
            if (_foldingManager != null)
            {
                foreach (var current in _foldingManager.AllFoldings)
                {
                    current.IsFolded = !current.IsFolded;
                }
            }
        }

        private void ChangeFoldStatus(bool isFolded)
        {
            foreach (var current in _foldingManager.AllFoldings)
            {
                current.IsFolded = isFolded;
            }
        }

        private void ShowDefinitions()
        {
            if (_foldingManager != null)
            {
                foreach (var current in _foldingManager.AllFoldings)
                {
                    current.IsFolded = (current.Tag is NewFolding);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /*
                private void SetWatcher()
                {
                    string directoryName = Path.GetDirectoryName(Filename);
                    if (directoryName == null || !Directory.Exists(directoryName))
                    {
                    }
                }
        */

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

        /*
                [Localizable(false)]
                private void InsertSnippet()
                {
                    var snippetReplaceableTextElement = new SnippetReplaceableTextElement {Text = "i"};
                    var snippet = new Snippet
                    {
                        Elements =
                        {
                            new SnippetTextElement
                            {
                                Text = "for "
                            },
                            new SnippetReplaceableTextElement
                            {
                                Text = "item"
                            },
                            new SnippetTextElement
                            {
                                Text = " in range("
                            },
                            new SnippetReplaceableTextElement
                            {
                                Text = "from"
                            },
                            new SnippetTextElement
                            {
                                Text = ", "
                            },
                            new SnippetReplaceableTextElement
                            {
                                Text = "to"
                            },
                            new SnippetTextElement
                            {
                                Text = ", "
                            },
                            new SnippetReplaceableTextElement
                            {
                                Text = "step"
                            },
                            new SnippetTextElement
                            {
                                Text = "):backN\t"
                            },
                            new SnippetSelectionElement()
                        }
                    };
                    snippet.Insert(TextArea);
                }
        */

        private void TextEditorGotFocus(object sender, RoutedEventArgs e)
        {
            DocumentViewModel.Instance.TextBox = this;
            OnPropertyChanged("Line");
            OnPropertyChanged("Column");
            OnPropertyChanged("Offset");
            OnPropertyChanged("RobotType");
            FileSave = ((!string.IsNullOrEmpty(Filename))
                ? File.GetLastWriteTime(Filename).ToString(CultureInfo.InvariantCulture)
                : string.Empty);
        }

        protected internal virtual char[] GetWordParts()
        {
            return new char[0];
        }

    }
}
