using GalaSoft.MvvmLight.Messaging;

namespace miRobotEditor.Core
{
    public class CommandMessage:MessageBase
    {
        public string Command { get; set; }
    }
}
