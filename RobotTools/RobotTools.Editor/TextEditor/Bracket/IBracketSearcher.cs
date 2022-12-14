using ICSharpCode.AvalonEdit.Document;

namespace RobotTools.Editor.TextEditor.Bracket
{
    public interface IBracketSearcher
    {
        BracketSearchResult SearchBracket(TextDocument document, int offset);
    }
}
