namespace miRobotEditor.EditorControl.Interfaces
{	/// <summary>
    /// An item in the insight window.
    /// </summary>
    public interface IInsightItem
    {
        object Header { get; }
        object Content { get; }
    }
}
