namespace miRobotEditor.UI.Controls
{
    /// <summary>
    /// Specifies different collapse modes of a ExtendedGridSplitter.
    /// </summary>
    public enum GridSplitterCollapseMode
    {
        /// <summary>
        /// The ExtendedGridSplitter cannot be collapsed or expanded.
        /// </summary>
        None = 0,
        /// <summary>
        /// The column (or row) to the right (or below) the
        /// splitter's column, will be collapsed.
        /// </summary>
        Next = 1,
        /// <summary>
        /// The column (or row) to the left (or above) the
        /// splitter's column, will be collapsed.
        /// </summary>
        Previous = 2
    }
}