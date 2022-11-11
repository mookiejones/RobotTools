using RobotTools.UI.Editor.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTools.UI.Editor
{
    public partial class AvalonEditor
    {
        private void AddBookMark(int lineNumber, BitmapImage img)
        {
            var image = new BookmarkImage(img);
            _iconBarManager.Bookmarks.Add(new ClassMemberBookmark(lineNumber, image));

           

        }

      
    }
}
