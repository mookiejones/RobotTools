using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTools.UI.Editor
{
    public static class Global
    {
        public static string StartupPath
        {
            get { return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName); }
        }
    }
}
