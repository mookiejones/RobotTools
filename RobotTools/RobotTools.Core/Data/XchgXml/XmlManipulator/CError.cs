using System;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CError
    {
        public static bool WithErrorWindow;

        public static void Init(bool withErrorWindow)
        {
            WithErrorWindow = withErrorWindow;
        }

        public static void SetError(string errorText)
        {
           // Messages.Add("#" + errorText);
            Console.WriteLine(errorText);
            if (WithErrorWindow)
            {
              //  MessageBox.Show(errorText, "XchXml Error");
            }
        }
    }
}
