using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using RobotTools.Editor.TextEditor.Bookmarks;

namespace RobotTools.Editor.TextEditor.IconBar
{
    public sealed class IconBarManager : IBookmarkMargin
    {
        private readonly ObservableCollection<IBookmark> _bookmarks = new ObservableCollection<IBookmark>();

        public IconBarManager()
        {
            _bookmarks.CollectionChanged += BookmarksCollectionChanged;
        }

        public event EventHandler RedrawRequested;

        public IList<IBookmark> Bookmarks => _bookmarks;

        public void Redraw()
        {
            if (RedrawRequested != null)
            {
                RedrawRequested(this, EventArgs.Empty);
            }
        }

        private void BookmarksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Redraw();
        }

        public void AddBookMark(UIElement item)
        {
        }
    }
}
