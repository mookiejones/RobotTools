using System.Windows.Input;
using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.EditorControl.Classes
{
    /// <summary>
    /// Description of ClassMemberBookmark.
    /// </summary>
    public class ClassMemberBookmark : IBookmark
    {

        private readonly int _linenumber;
        private readonly IImage _image;
        public ClassMemberBookmark(int lineNumber, IImage image)
        {
            _image = image;
            _linenumber = lineNumber;
        }



        public int LineNumber
        {
            get
            {
                return _linenumber;
            }
        }

        public IImage Image
        {
            get
            {
                return _image;
            }
        }

        public int ZOrder
        {
            get
            {
                return -10;
            }
        }

        public bool CanDragDrop
        {
            get
            {
                return false;
            }
        }

        public void MouseDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                //	var f = Services.AnalyticsMonitorService.TrackFeature("ICSharpCode.SharpDevelop.Bookmarks.ClassMemberBookmark.ShowContextMenu");
                //TODO Fix this

                //var ctx = MenuService.ShowContextMenu(e.Source as UIElement, this, ContextMenuPath);
                //		ctx.Closed += delegate { f.EndTracking(); };
                e.Handled = true;
            }
        }

        public void MouseUp(MouseButtonEventArgs e)
        {
        }

        public void Drop(int lineNumber)
        {
        }
    }

}
