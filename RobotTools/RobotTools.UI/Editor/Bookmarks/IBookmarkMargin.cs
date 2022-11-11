using System;
using System.Collections.Generic;
using CSITextAnchor = ICSharpCode.AvalonEdit.Document.ITextAnchor;
namespace RobotTools.UI.Editor.Bookmarks
{
    public interface IBookmarkMargin
    {
        IList<IBookmark> Bookmarks { get; }
        event EventHandler RedrawRequested;
        void Redraw();
    }
}
