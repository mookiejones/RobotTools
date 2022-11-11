using System;
using System.Collections;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CCLParameters
    {
        public string OperateFile = "";

        public string InputFile = "";

        public string UpgFile = "";

        public bool WithErrorWindow;

        public CCLParameters(string cmdLine)
        {
            CCLParser parameters = new CCLParser(cmdLine);
            SetParameters(parameters);
        }

        public void SetParameters(CCLParser parser)
        {
            foreach (DictionaryEntry item in parser)
            {
                switch ((string)item.Key)
                {
                    case "i":
                        InputFile = (string)item.Value;
                        break;
                    case "u":
                        UpgFile = (string)item.Value;
                        break;
                    case "w":
                        WithErrorWindow = true;
                        break;
                    case "filename":
                        OperateFile = (string)item.Value;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }
    }
}
