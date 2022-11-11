using RobotTools.Core.Data.XchgXml.XmlManipulator;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
namespace RobotTools.Core.Data.XchgXml.XmlCLInterface
{
    public  class CXmlCLInterface
    {
        private CXmlManipulator XmlManipulator;

        private CCLParameters Parameters;

        public CXmlCLInterface(CCLParameters CLParams)
        {
            Parameters = CLParams;
            XmlManipulator = new CXmlManipulator();
        }

        public bool Init()
        {
            if (Parameters.OperateFile.Equals(""))
            {
                CError.SetError("Missing operate file");
                return false;
            }
            if (Parameters.InputFile.Equals(""))
            {
                CError.SetError("Missing input file");
                return false;
            }
           
            if (!XmlManipulator.Init(Parameters.OperateFile, Parameters.InputFile, Parameters.UpgFile, Parameters.WithErrorWindow))
            {
                return false;
            }
            return true;
        }

        public bool Exit()
        {
            if (!XmlManipulator.SaveFiles(Parameters.OperateFile))
            {
                return false;
            }
            return true;
        }

        public bool Xchg()
        {
            if (!XmlManipulator.Xchg())
            {
                return false;
            }
            return true;
        }

        public static int Execute(IEnumerable<string> args)
        {
            string commandLine = "\"C:\\Programming\\XchgXml\\bin\\Debug\\net20\\XchgXml.exe\"  C:\\krc\\SmartHMI\\Config\\Authentication.config /i=c:\\Modify\\ChangeValue.xml";// Environment.CommandLine;
            string text=args.First().Replace("\\", "\\\\");
           
            string pattern = "xchgxml(\\.exe)?\"?\\s+\"?" + text;
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(commandLine);
            if (match.Success)
            {
                string[] array = commandLine.Substring(match.Index).Split(new char[1] { ' ' }, 2);
                if (array.Length == 2)
                {
                    string cmdLine = array[1].Trim();
                    CCLParameters cLParams;
                    try
                    {
                        cLParams = new CCLParameters(cmdLine);
                    }
                    catch (ArgumentException)
                    {
                        CError.SetError("XchgXml: Invalid command line.");
                        return -1;
                    }
                    CXmlCLInterface cXmlCLInterface = new CXmlCLInterface(cLParams);
                    if (!cXmlCLInterface.Init())
                    {
                        return -1;
                    }
                    if (!cXmlCLInterface.Xchg())
                    {
                        return -1;
                    }
                    if (!cXmlCLInterface.Exit())
                    {
                        return -1;
                    }
                    return 0;
                }
                CError.SetError("Invalid commandline: " + commandLine);
                return -1;
            }
            CError.SetError("Invalid commandline: " + commandLine);
            return -1;
        }
       


    }
}
