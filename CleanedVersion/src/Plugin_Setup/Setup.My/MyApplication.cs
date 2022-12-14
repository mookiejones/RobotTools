using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
namespace Setup.My
{
	[GeneratedCode("MyTemplate", "8.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
	internal class MyApplication : WindowsFormsApplicationBase
	{
		[EditorBrowsable(EditorBrowsableState.Advanced), DebuggerHidden, STAThread]
		internal static void Main(string[] Args)
		{
			try
			{
				Application.SetCompatibleTextRenderingDefault(WindowsFormsApplicationBase.UseCompatibleTextRendering);
			}
			finally
			{
			}
			MyProject.Application.Run(Args);
		}
		private void MyApplication_Startup(object sender, StartupEventArgs e)
		{
			MyProject.Forms.frmMain.Job = FrmMain.JobType.NoJob;
			checked
			{
				if (e.CommandLine.Count > 0)
				{
					int arg_30_0 = 0;
					int num = e.CommandLine.Count - 1;
					for (int i = arg_30_0; i <= num; i++)
					{
						if (e.CommandLine[i].Contains("install"))
						{
							MyProject.Forms.frmMain.Job = FrmMain.JobType.Install2;
						}
						if (e.CommandLine[i].Contains("uninstall"))
						{
							MyProject.Forms.frmMain.Job = FrmMain.JobType.Uninstall2;
						}
						if (e.CommandLine[i].Contains("update"))
						{
							MyProject.Forms.frmMain.Job = FrmMain.JobType.Update2;
						}
						if (e.CommandLine[i].Contains("reinstall"))
						{
							MyProject.Forms.frmMain.Job = FrmMain.JobType.Reinstall2;
						}
					}
				}
			}
		}
		private void MyApplication_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
		{
		}
		private void MyApplication_UnhandledException(object sender, Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
		{
			Interaction.MsgBox(e.Exception.Message, MsgBoxStyle.OkOnly, null);
		}
		[DebuggerStepThrough]
		public MyApplication() : base(AuthenticationMode.Windows)
		{
			base.StartupNextInstance += new StartupNextInstanceEventHandler(this.MyApplication_StartupNextInstance);
			base.Startup += new StartupEventHandler(this.MyApplication_Startup);
			base.UnhandledException += new Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventHandler(this.MyApplication_UnhandledException);
			this.IsSingleInstance = false;
			this.EnableVisualStyles = true;
			this.SaveMySettingsOnExit = true;
			this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
		}
		[DebuggerStepThrough]
		protected override void OnCreateMainForm()
		{
			this.MainForm = MyProject.Forms.frmMain;
		}
	}
}
