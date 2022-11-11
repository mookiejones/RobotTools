using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTools.Core.Utilities
{
    public static class PathEx
    {
        /// <summary>
        /// Used to help prevent from freezing when network directory doesnt exist
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>  
        public static bool DoesDirectoryExist(this FileInfo file)
        {
            
            if (file.DirectoryName != null)
            {
                var d = new DirectoryInfo(file.DirectoryName);

                try
                {
                    if (Directory.GetDirectories(d.Root.ToString()).Length > 0)
                        return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
