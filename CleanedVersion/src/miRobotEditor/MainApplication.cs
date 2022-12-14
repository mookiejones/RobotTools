using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using miRobotEditor.Core.Classes.Messaging;
using miRobotEditor.ViewModels;

namespace miRobotEditor
{

    public class MainApplication:Application
    {

        private const string Unique = "miRobotEditorApplication";

        public static string StartupPath
        {
            get
            {
                return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            }
        }

        public static string Version
        {
            get
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                return executingAssembly.GetName().Version.ToString();
            }
        }
        public static string ProductName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().ToString();
            }
        }
        [Localizable(false)]
        private static bool CheckEnvironment()
        {
            bool result;
            if (Environment.Version < new Version(4, 0, 30319))
            {
                MessageBox.Show(string.Format("This version of {0} requires .Net 4.0. You are using {1}.", Assembly.GetExecutingAssembly().GetName().Name, Environment.Version));
                result = false;
            }
            else
            {
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows, Environment.SpecialFolderOption.DoNotVerify);
                if (Environment.GetEnvironmentVariable("WINDIR") != folderPath)
                {
                    Environment.SetEnvironmentVariable("WINDIR", folderPath);
                }
                result = true;
            }
            return result;
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            base.MainWindow.Activate();
            var instance = ServiceLocator.Current.GetInstance<MainViewModel>();
            instance.LoadFile(args);
            return true;
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

            var msg = new ErrorMessage("App", e.Exception);
            Messenger.Default.Send(msg);
            Console.Write(e);
            e.Handled = true;
        }

    
    
    }
}
