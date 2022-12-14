using System;
using GalaSoft.MvvmLight.Messaging;

namespace miRobotEditor.Core.Classes.Messaging
{
    public sealed class ErrorMessage:MessageBase
    {
// ReSharper disable once MemberCanBePrivate.Global
        public string Title { get; set; }
// ReSharper disable once MemberCanBePrivate.Global
        public Exception Exception { get; set; }

        public ErrorMessage(string title, Exception exception)
        {
            
            Title = title;
            Exception = exception;

        }
    }
}
