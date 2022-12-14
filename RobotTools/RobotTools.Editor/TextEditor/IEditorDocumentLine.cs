using ICSharpCode.AvalonEdit.Document;

namespace RobotTools.Editor.TextEditor
{
    public interface IEditorDocumentLine : IDocumentLine
    {
        string Text { get; }
    }
}
