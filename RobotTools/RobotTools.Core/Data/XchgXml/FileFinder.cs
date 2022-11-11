using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Zip;

namespace RobotTools.Core.Data.XchgXml
{
    public class FileFinder
    {
        public static void FindFiles(string directory)
        {
            if (!Directory.Exists(directory))
                return;

            var dir = new DirectoryInfo(directory);

            // Find Zip Files
            var zipFiles = dir.EnumerateFiles("*.zip");
            foreach (var zip in zipFiles)
                ParseZipFile(zip.FullName);

        }

        public static void ParseZipFile(string path)
        {
            var zipFile = new ZipFile(path);

            foreach(var entry in zipFile.Entries.Where(o=>o.FileName.Contains("Authentication")))

            {

            }

        }
    }
}
