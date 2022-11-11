using System;
using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    internal class CHelper
    {
        public static bool OpenSingleFile(string filename, XmlDocument doc)
        {
            try
            {
                doc.Load(filename);
            }
            catch (Exception ex)
            {
                CError.SetError(ex.Message);
                return false;
            }
            return true;
        }
    }
}
