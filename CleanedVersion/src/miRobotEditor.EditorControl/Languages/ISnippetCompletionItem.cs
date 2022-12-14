namespace miRobotEditor.EditorControl.Languages
{
    public interface ISnippetCompletionItem : ICompletionItem
    {
        string Keyword { get; }
    }
}