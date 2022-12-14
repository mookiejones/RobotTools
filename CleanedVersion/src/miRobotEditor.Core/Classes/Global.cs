using System;
using System.ComponentModel;
using System.IO;

namespace miRobotEditor.Core.Classes
{
    /// <summary>
    /// Global Variables
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// XML Configuration File For Docking Manager
        /// </summary>
        public const string DockConfigPath = "dockConfig.xml";

        [Localizable(false)]
        public static string DockConfig { get { return AppDomain.CurrentDomain.BaseDirectory + DockConfigPath; } }

        public static string SettingsFileName { get; set; }

        /// <summary>
        /// Used to help prevent from freezing when network directory doesnt exist
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>  
        public static bool DoesDirectoryExist(string filename)
        {
            var f = new FileInfo(filename);
            if (f.DirectoryName != null)
            {
                var d = new DirectoryInfo(f.DirectoryName);

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



        /// <summary>
        /// Log File
        /// </summary>
        public const string LogFile = "logFile.txt";

        /// <summary>
        /// Constant Error Image
        /// </summary>
        public const string ImgError = "..\\..\\Resources\\error.png";

        /// <summary>
        /// Constant Error Info
        /// <remarks>
        /// Used with Message Window</remarks>
        /// </summary>
        public const string ImgInfo = "..\\..\\Resources\\info.png";

        /// <summary>
        /// Constant ObjectBrowser Icon
        /// </summary>
        public const string IconObjectBrowser = "pack://application:,,/Resources/objectbrowser.png";

        /// <summary>
        /// Constant ObjectBrowser Icon
        /// </summary>
        public const string IconProperty = "pack://application:,,/Resources/property-blue.png";

        /// <summary>
        /// Constant Variable Image
        /// </summary>
        public const string ImgConst = "..\\..\\Resources\\vxconstant_icon.png";
        /// <summary>
        /// Struct Variable Image
        /// </summary>
        public const string ImgStruct = "..\\..\\Resources\\vxstruct_icon.png";
        /// <summary>
        /// Method Variable Image
        /// </summary>
        public const string ImgMethod = "..\\..\\Resources\\vxmethod_icon.png";
        /// <summary>
        /// Enum Variable Image
        /// </summary>
        public const string ImgEnum = "..\\..\\Resources\\vxenum_icon.png";
        /// <summary>
        /// Field Variable Image
        /// </summary>
        public const string ImgField = "..\\..\\Resources\\vxfield_icon.png";
        /// <summary>
        /// Value Variable Image
        /// </summary>
        public const string ImgValue = "..\\..\\Resources\\vxvaluetype_icon.png";
        /// <summary>
        /// Signal Variable Image
        /// </summary>
        public const string ImgSignal = "..\\..\\Resources\\vxevent_icon.png";
        /// <summary>
        /// XYZ Position Variable Image
        /// </summary>
        public const string ImgXyz = "..\\..\\Resources\\vxXYZ_icon.png";
        /// <summary>
        /// Source File Image
        /// </summary>
        public const string ImgSrc = "..\\..\\Resources\\srcfile.png";
        /// <summary>
        /// Dat File Image
        /// </summary>
        public const string ImgDat = "..\\..\\Resources\\datfile.png";
        /// <summary>
        /// SPS File Image
        /// </summary>
        public const string ImgSps = "..\\..\\Resources\\spsfile.png";




        public class Options
        {
            static Options()
            {
                FileOptions = new FileOptions();
            }

            public static FileOptions FileOptions { get; set; }
        }

        public class FileOptions
        {
            public bool ShowFullName { get; set; }
        }

    }
}
