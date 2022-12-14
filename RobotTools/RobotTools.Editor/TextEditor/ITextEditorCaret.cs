using System;

namespace RobotTools.Editor.TextEditor
{
    public interface ITextEditorCaret
    {
        int Offset { get; set; }
        int Line { get; set; }
        int Column { get; set; }
        Location Position { get; set; }
        event EventHandler PositionChanged;
    }
}
