using ICSharpCode.AvalonEdit.Document;

namespace miRobotEditor.EditorControl.Classes
{
    public sealed class StringTextBuffer : TextSourceAdapter
    {
        public StringTextBuffer(string text)
            : base(new StringTextSource(text))
        {
        }
    }
}