using System;
namespace RobotTools.UI.Editor
{
    public interface ICompletionListWindow
    {
        double Width { get; set; }
        double Height { get; set; }
        bool CloseAutomatically { get; set; }
        int StartOffset { get; set; }
        int EndOffset { get; set; }
        event EventHandler Closed;
        void Close();
    }
}
