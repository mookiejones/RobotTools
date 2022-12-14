using RobotTools.Editor.TextEditor.Bookmarks;

namespace RobotTools.Editor.TextEditor.Completion
{
    public interface ICompletionItem
    {
        string Text { get; }
        string Description { get; }
        IImage Image { get; }
        double Priority { get; }
        void Complete(CompletionContext context);
    }
}
