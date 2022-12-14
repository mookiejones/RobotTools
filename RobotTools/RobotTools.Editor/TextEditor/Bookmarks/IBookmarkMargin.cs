using System;
using System.Collections.Generic;

namespace RobotTools.Editor.TextEditor.Bookmarks
{
    public interface IBookmarkMargin
    {
        IList<IBookmark> Bookmarks { get; }
        event EventHandler RedrawRequested;
        void Redraw();
    }
}
