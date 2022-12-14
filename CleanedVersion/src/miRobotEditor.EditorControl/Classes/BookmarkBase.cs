using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using miRobotEditor.Core.Interfaces;
using miRobotEditor.EditorControl.Interfaces;


namespace miRobotEditor.EditorControl.Classes
{
    /// <summary>
    /// A bookmark that can be attached to an AvalonEdit TextDocument.
    /// </summary>
    public class BookmarkBase : IBookmark
    {
        Location _location;

        IEditor _document;

        public IEditor Document
        {
            get
            {
                return _document;
            }
            set
            {
                if (_document == value) return;
                if (Anchor != null)
                {
                    _location = Anchor.Location;
                    Anchor = null;
                }
                _document = value;
                CreateAnchor();
                OnDocumentChanged(EventArgs.Empty);
            }
        }

        void CreateAnchor()
        {
            if (_document != null)
            {
                var lineNumber = Math.Max(1, Math.Min(_location.Line, _document.TotalNumberOfLines));
                var lineLength = _document.GetLine(lineNumber).Length;
                var offset = _document.PositionToOffset(
                    lineNumber,
                    Math.Max(1, Math.Min(_location.Column, lineLength + 1))
                );
                Anchor = _document.CreateAnchor(offset);
                // after insertion: keep bookmarks after the initial whitespace (see DefaultFormattingStrategy.SmartReplaceLine)
                Anchor.MovementType = AnchorMovementType.AfterInsertion;
                Anchor.Deleted += AnchorDeleted;
            }
            else
            {
                Anchor = null;
            }
        }

        void AnchorDeleted(object sender, EventArgs e)
        {
            // the anchor just became invalid, so don't try to use it again
            _location = Location.Empty;
            Anchor = null;
            RemoveMark();
        }

        protected virtual void RemoveMark()
        {
            if (_document == null) return;
            var bookmarkMargin = _document.GetService(typeof(IBookmarkMargin)) as IBookmarkMargin;
            if (bookmarkMargin != null)
                bookmarkMargin.Bookmarks.Remove(this);
        }

        /// <summary>
        /// Gets the TextAnchor used for this bookmark.
        /// Is null if the bookmark is not connected to a document.
        /// </summary>
        public ITextAnchor Anchor { get; private set; }

        public Location Location
        {
            get
            {
                return Anchor != null ? Anchor.Location : _location;
            }
            set
            {
                _location = value;
                CreateAnchor();
            }
        }

        public event EventHandler DocumentChanged;

        protected virtual void OnDocumentChanged(EventArgs e)
        {
            if (DocumentChanged != null)
            {
                DocumentChanged(this, e);
            }
        }

        protected virtual void Redraw()
        {
            if (_document == null) return;
            var bookmarkMargin = _document.GetService(typeof(IBookmarkMargin)) as IBookmarkMargin;
            if (bookmarkMargin != null)
                bookmarkMargin.Redraw();
        }

        public int LineNumber
        {
            get
            {
                return Anchor != null ? Anchor.Line : _location.Line;
            }
        }

        public int ColumnNumber
        {
            get
            {
                return Anchor != null ? Anchor.Column : _location.Column;
            }
        }

        public virtual int ZOrder
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets if the bookmark can be toggled off using the 'set/unset bookmark' command.
        /// </summary>
        public virtual bool CanToggle
        {
            get
            {
                return true;
            }
        }

        public BookmarkBase(Location location)
        {
            Location = location;
        }

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        // ReSharper disable InconsistentNaming
        public static readonly IImage defaultBookmarkImage = null;// = new ResourceServiceImage("Bookmarks.ToggleMark");
        // ReSharper restore InconsistentNaming

        public static IImage DefaultBookmarkImage
        {
            get { return defaultBookmarkImage; }
        }

        public virtual IImage Image
        {
            get { return defaultBookmarkImage; }
        }

        public virtual void MouseDown(MouseButtonEventArgs e)
        {
        }

        public virtual void MouseUp(MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left || !CanToggle) return;
            RemoveMark();
            e.Handled = true;
        }

        public virtual bool CanDragDrop
        {
            get { return false; }
        }

        public virtual void Drop(int lineNumber)
        {
        }
    }

}
