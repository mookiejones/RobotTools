using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RobotTools.Core.Messages;
using RobotTools.ViewModels;
using System;

namespace RobotTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow  
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<MainViewModel>();

#if DESIGNER
            SendDesignMessages();
#endif
        }

        private void SendDesignMessages()
        {
            for(var i = 0; i < 20; i++)
            {
                var message = $"Sample Message {i}";
                var title = $"Sample Title{i}";

                var msg = new SampleMessage(title, message);

                // Send a message from some other module
                WeakReferenceMessenger.Default.Send<IMessageBase>(msg);
            }
        }
    }
}
