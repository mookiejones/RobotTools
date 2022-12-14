using GalaSoft.MvvmLight.Messaging;

namespace miRobotEditor.Core.Classes.Messaging
{
    public sealed class FileMessage:MessageBase
    {

        public FileMessage()
        {
            
        }

        public FileMessage(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public string Path { get; set; }

    }
}
