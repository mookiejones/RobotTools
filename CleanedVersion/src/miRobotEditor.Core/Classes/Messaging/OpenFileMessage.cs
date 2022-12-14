using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.Core.Classes.Messaging
{
    public sealed class OpenFileMessage
    {
        public IVariable Variable { get; set; }
        public OpenFileMessage(IVariable value)
        {
            Variable = value;

        }
    }
}