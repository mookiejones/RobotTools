using System.Windows.Input;

namespace miRobotEditor.Core.Interfaces
{
    /// <summary>
    /// Represents a bookmark in the bookmark margin.
    /// </summary>
    public interface IBookmark
    {
        /// <summary>
        /// Gets the line number of the bookmark.
        /// </summary>
        int LineNumber { get; }

        /// <summary>
        /// Gets the image.
        /// </summary>
        IImage Image { get; }

        /// <summary>
        /// Gets the Z-Order of the bookmark icon.
        /// </summary>
        int ZOrder { get; }

        /// <summary>
        /// Handles the mouse down event.
        /// </summary>
        void MouseDown(MouseButtonEventArgs e);

        /// <summary>
        /// Handles the mouse up event.
        /// </summary>
        void MouseUp(MouseButtonEventArgs e);

        /// <summary>
        /// Gets whether this bookmark can be dragged around.
        /// </summary>
        bool CanDragDrop { get; }

        /// <summary>
        /// Notifies the bookmark that it was dropped on the specified line.
        /// </summary>
        void Drop(int lineNumber);
    }
}