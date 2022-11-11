using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using RobotTools.Core.Messages;
using System;
using System.Collections.ObjectModel;

namespace RobotTools.ViewModels
{
    public class MessageViewModel:ObservableRecipient
    {

        public ObservableCollection<IMessageBase> Messages { get; set; } = new ObservableCollection<IMessageBase>();
        public MessageViewModel()
        {
#if DESIGNER
CreateDesignData();
#endif
            // Register a message in some module
            WeakReferenceMessenger.Default.Register<IMessageBase>(this, (r, m) =>
            {
                Messages.Add(m);
                // Handle the message here, with r being the recipient and m being the
                // input message. Using the recipient passed as input makes it so that
                // the lambda expression doesn't capture "this", improving performance.
            });
        }

        private void CreateDesignData()
        {
           for(var i = 0;i<20;i++)
            {
                var message = new SampleMessage($"Message Title {i}", $"Sample Message {i}");
                Messages.Add(message);
            }
        }
    }
}
