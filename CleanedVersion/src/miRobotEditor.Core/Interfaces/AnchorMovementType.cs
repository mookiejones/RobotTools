namespace miRobotEditor.Core.Interfaces
{
    /// <summary>
    /// Defines how a text anchor moves.
    /// </summary>
    public enum AnchorMovementType
    {
        /// <summary>
        /// When text is inserted at the anchor position, the type of the insertion
        /// determines where the caret moves to. For normal insertions, the anchor will stay
        /// behind the inserted text.
        /// </summary>
        Default = ICSharpCode.AvalonEdit.Document.AnchorMovementType.Default,
        /// <summary>
        /// Behaves like a start marker - when text is inserted at the anchor position, the anchor will stay
        /// before the inserted text.
        /// </summary>
        BeforeInsertion = ICSharpCode.AvalonEdit.Document.AnchorMovementType.BeforeInsertion,
        /// <summary>
        /// Behave like an end marker - when text is insered at the anchor position, the anchor will move
        /// after the inserted text.
        /// </summary>
        AfterInsertion = ICSharpCode.AvalonEdit.Document.AnchorMovementType.AfterInsertion
    }
}