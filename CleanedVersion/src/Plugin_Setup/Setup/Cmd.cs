using System;
using System.Diagnostics;
namespace Setup
{
	public class Cmd
	{
		public static void RunCommandCom(string command, string arguments, bool permanent)
		{
			Process process = new Process();
			process.StartInfo = new ProcessStartInfo
			{
				Arguments = string.Concat(new string[]
				{
					" ",
					permanent ? "/K" : "/C",
					" ",
					command,
					" ",
					arguments
				}),
				FileName = "cmd.exe"
			};
			process.Start();
			process.WaitForExit(20000);
		}
	}
}
