using CommunityToolkit.Mvvm.DependencyInjection;
using RobotTools.ViewModels;
using System.Windows.Controls;

namespace RobotTools.Views
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class MessageView : UserControl
    {
        public MessageView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<MessageViewModel>();
        }
    }
}
