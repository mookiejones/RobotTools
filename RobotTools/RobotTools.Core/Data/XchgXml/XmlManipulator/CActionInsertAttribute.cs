using System;
using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CActionInsertAttribute : CActionBase
    {
        public CActionInsertAttribute(ref CActionParameters actionParameters, XmlDocument doc)
            : base(ref actionParameters, doc)
        {
        }

        public override bool DoIt()
        {
            if (ActionParameters.location != null)
            {
                FindLocNodes(true);
            }
            XmlAttribute xmlAttribute = Doc.CreateAttribute(ActionParameters.node.Attributes[0].Name);
            xmlAttribute.Value = ActionParameters.node.Attributes[0].Value;
            try
            {
                BaseNodes[0].Attributes.Append(xmlAttribute);
            }
            catch (Exception ex)
            {
                CError.SetError(ex.Message);
                return false;
            }
            return true;
        }

        public override void LogStart()
        {
            string text = ".Starting action INSERTATTRIBUTE";
            text = text + " path:" + ActionParameters.path;
            text = text + " postype:" + ActionParameters.posType;
            if (ActionParameters.identifier != null)
            {
                text = text + " identifier:" + ActionParameters.identifier;
            }
            text = text + " attribute:" + ActionParameters.node.Attributes[0].Name;
            Messages.Add(text);

        }
    }
}
