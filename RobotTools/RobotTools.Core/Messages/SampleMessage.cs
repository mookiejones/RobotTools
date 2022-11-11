using System;

namespace RobotTools.Core.Messages
{
    public class SampleMessage : IMessageBase
    {
        public string Title { get; private set; }


        public string Message { get; private set; }

        public DateTime Time => DateTime.Now;

        public SampleMessage(string title,string message)
        {
            Title = title;
            Message = message;

        }

        public SampleMessage()
        {

        }
    }
}
