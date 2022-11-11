using CommunityToolkit.Mvvm.DependencyInjection;
using RobotTools.ViewModels;

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
        }
    }
}
