using System.Diagnostics;

namespace miRobotEditor.ViewModels
{
    /// <summary>
    /// Description of FTPFolder.
    /// </summary>
    public class FTPFolder
    {

        public string Path { get; set; }
        public string Name { get; set; }
        //
        // Returns the folder name without having any path information
        //		
        [DebuggerStepThrough]
        public static string SafeFolderName(string path)
        {
            var fileParts = path.Split('/');
            return fileParts[fileParts.Length - 1];
        }
    }
}