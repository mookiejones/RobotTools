using System;
using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CXmlManipulator
    {
        private XmlDocument docOperate = new XmlDocument();

        private XmlDocument docInput = new XmlDocument();

        private XmlDocument docUpg = new XmlDocument();

        public bool Init(string fileOperate, string fileInput, string fileUpg, bool withErrorWindow)
        {
            CError.Init(withErrorWindow);
            if (!CHelper.OpenSingleFile(fileOperate, docOperate))
            
                return false;
             
            if (!CHelper.OpenSingleFile(fileInput, docInput))
            
                return false;
             
            if (!fileUpg.Equals("") && !CHelper.OpenSingleFile(fileUpg, docUpg))
             
                return false;
             
            return true;
        }

        public bool SaveFiles(string fileOperate)
        {
            try
            {
                XmlTextWriter xmlTextWriter = new XmlTextWriter(fileOperate, null);
                xmlTextWriter.Formatting = Formatting.Indented;
                docOperate.WriteContentTo(xmlTextWriter);
                xmlTextWriter.Close();
            }
            catch (Exception ex)
            {
                CError.SetError("Error on saving " + fileOperate + ": " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Xchg()
        {
            XmlNodeList xmlNodeList = docInput.SelectNodes("//XchgXmlInput/action");
            foreach (XmlNode item in xmlNodeList)
            {
                if (!DoSingleAction(item))
                {
                    return false;
                }
            }
            return true;
        }

        private bool DoSingleAction(XmlNode node)
        {
            CActionParameters actionParameters = new CActionParameters();
            if (!actionParameters.Set(node))
            {
                return false;
            }
            if (node.FirstChild.Name != "type")
            {
                return false;
            }
            CActionBase cActionBase2;
            switch (node.FirstChild.InnerText.ToUpper())
            {
                case "INSERTNODE":
                    cActionBase2 = new CActionInsertNode(ref actionParameters, docOperate);
                    break;
                case "INSERTATTRIBUTE":
                    cActionBase2 = new CActionInsertAttribute(ref actionParameters, docOperate);
                    break;
                case "REMOVENODE":
                    cActionBase2 = new CActionRemoveNode(ref actionParameters, docOperate);
                    break;
                case "CHANGEVAL":
                    cActionBase2 = new CActionChangeVal(ref actionParameters, docOperate);
                    break;
                case "UPGRADEVAL":
                    {
                        CActionBase cActionBase = new CActionPrepUpgradeVal(ref actionParameters, docUpg);
                        if (!cActionBase.Init())
                        {
                            return false;
                        }
                        if (!cActionBase.DoIt())
                        {
                            return false;
                        }
                        cActionBase2 = new CActionChangeVal(ref actionParameters, docOperate);
                        break;
                    }
                default:
                    CError.SetError("Unknown action type: " + node.FirstChild.InnerText);
                    return false;
            }
            if (!cActionBase2.Init())
            {
                return false;
            }
            if (!cActionBase2.DoIt())
            {
                return false;
            }
            return true;
        }
    }
}
