using System.Windows.Media.Imaging;
using miRobotEditor.Core.Classes.Messaging.Interfaces;

namespace miRobotEditor.Core.Classes.Messaging
{
    public sealed class OutputWindowMessage : IMessage
    {
        public string Time { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public BitmapImage Icon { get; set; }


        public OutputWindowMessage()
        {
        }


        private void SetIcon(MsgIcon icon)
        {
                   switch (icon)
            {
                case MsgIcon.Error:
                    Icon = Utilities.LoadBitmap(Global.ImgError);
                    break;
                case MsgIcon.Info:
                    Icon = Utilities.LoadBitmap(Global.ImgInfo);
                    break;
            }
        }
        public OutputWindowMessage(string title, string description, MsgIcon icon)
        {
            Title = title;
            Description = description;
            SetIcon(icon);


        }


    }
}
