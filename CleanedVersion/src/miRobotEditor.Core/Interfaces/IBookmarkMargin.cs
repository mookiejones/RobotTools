using System;
using System.Collections.Generic;

namespace miRobotEditor.Core.Interfaces

{

    /// <summary>
    /// The bookmark margin.
    /// </summary>
    public interface IBookmarkMargin
    {
        /// <summary>
        /// Gets the list of bookmarks.
        /// </summary>
        IList<IBookmark> Bookmarks { get; }

        /// <summary>
        /// Redraws the bookmark margin. Bookmarks need to call this method when the Image changes.
        /// </summary>
        void Redraw();

        event EventHandler RedrawRequested;
    }
}
