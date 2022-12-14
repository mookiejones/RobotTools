using System.Diagnostics;
using System.IO;

namespace RobotTools.Editor.TextEditor
{
    public static class Global
    {
        public static string StartupPath => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName);
    }
}
