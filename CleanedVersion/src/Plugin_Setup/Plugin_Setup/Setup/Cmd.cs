using System.Diagnostics;

namespace Plugin_Setup.Setup
{
    public class Cmd
    {
        public static void RunCommandCom(string command, string arguments, bool permanent)
        {
            var process = new Process();
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
