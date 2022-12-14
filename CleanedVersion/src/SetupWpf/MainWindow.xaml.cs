using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Path = System.Windows.Shapes.Path;

namespace SetupWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Fields

        private Timer tDoJob;
        private Timer tEnd;
        public JobType Job;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }

        public enum JobType
        {
            Install1,
            Install2,
            Update1,
            Update2,
            Reinstall1,
            Reinstall2,
            UnInstall1,
            Uninstall2,
            NoJob
        }
        private enum LogType
        {
            Info,
            Warning,
            Exception
        }

      

        private void Continue_Clicked(object sender, RoutedEventArgs e)
        {
//            this.pnl1.BringToFront();
            tDoJob.Start();

        }

        private void btnReboot_Click(object sender, RoutedEventArgs e)
        {

            Cmd.RunCommandCom("START /WAIT C:\\KRC\\StartKrc.exe /x", "", false);
            Cmd.RunCommandCom("START /B /WAIT C:\\KRC\\VxWin\\UploadRTOS.exe -faststart -disable -nosleep -nowait", "", false);
            Process.Start("shutdown", "-r -t 00");
            this.Close();

        }


        // Setup.frmMain
        private bool IsCross3Running()
        {
            bool result;
            try
            {
                var processesByName = Process.GetProcessesByName("Cross3");
                var num = 0;
                if (num >= processesByName.Length)
                {
                    result = false;
                }
                else
                {
                    var process = processesByName[num];
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        private void DelDir(string Dir)
        {
            try
            {
                object arg_43_0 = null;
                Type arg_43_1 = typeof(string);
                string arg_43_2 = "Format";
                object[] array = new object[]
		{
			RuntimeHelpers.GetObjectValue(Language.s("delete directory {0}")),
			Dir
		};
                var arg_43_3 = array;
                string[] arg_43_4 = null;
                Type[] arg_43_5 = null;
                var array2 = new[]
		{
			false,
			true
		};
                object arg_6E_0 = NewLateBinding.LateGet(arg_43_0, arg_43_1, arg_43_2, arg_43_3, arg_43_4, arg_43_5, array2);
                if (array2[1])
                {
                    Dir = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                }
                this.LogLine(Conversions.ToString(arg_6E_0), frmMain.LogType.Info);
                if (!Directory.Exists(Dir))
                {
                    this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
			{
				RuntimeHelpers.GetObjectValue(Language.s("directory {0} does not exist")),
				Path.GetFileName(Dir)
			}, null, null, null)), frmMain.LogType.Info);
                }
                else
                {
                    Directory.Delete(Dir, true);
                }
            }
            catch (Exception expr_DC)
            {
                ProjectData.SetProjectError(expr_DC);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
    }
}
