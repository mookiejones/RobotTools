using System;
using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;

namespace RobotTools.Editor.TextEditor
{
    public interface IInsightWindow : ICompletionWindow
    {
        IList<IInsightItem> Items { get; }
        IInsightItem SelectedItem { get; set; }
        event EventHandler<TextChangeEventArgs> DocumentChanged;
        event EventHandler SelectedItemChanged;
        event EventHandler CaretPositionChanged;
    }
}
