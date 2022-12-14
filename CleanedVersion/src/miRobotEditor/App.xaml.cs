using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Threading;

namespace miRobotEditor
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();

            

        }

        private static void HandleError(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Exception", e.ToString());

         
            e.Handled = true;
        }

        private void Application_Deactivated(object sender, EventArgs e)
        {

        }

        private void Application_Activated(object sender, EventArgs e)
        {

        }

        private void Application_FragmentNavigation(object sender, FragmentNavigationEventArgs e)
        {

        }
    }
}