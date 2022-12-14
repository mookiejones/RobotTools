using System.Windows.Media.Imaging;

namespace miRobotEditor.Core.Classes.Messaging.Interfaces
{
    public interface IMessage
    {
        BitmapImage Icon { get; }
        string Time { get; set; }
        string Title { get; set; }
        string Description { get; set; }
    }
}
