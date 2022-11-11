using System;
using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CActionInsertNode : CActionBase
    {
        public CActionInsertNode(ref CActionParameters actionParameters, XmlDocument doc)
            : base(ref actionParameters, doc)
        {
        }

        public override bool DoIt()
        {
            XmlNode xmlNode = null;
            if (ActionParameters.location != null)
            {
                FindLocNodes(true);
            }
            switch (ActionParameters.posType)
            {
                case PosType.Last:
                case PosType.First:
                    xmlNode = null;
                    break;
                case PosType.After:
                    if (MatchingNodes.Count == 0)
                    {
                        ActionParameters.posType = PosType.Before;
                        xmlNode = null;
                        break;
                    }
                    try
                    {
                        xmlNode = BaseNodes[0].SelectSingleNode(CombineXPath(ActionParameters.path, ActionParameters.identifier), NameSpaceManager);
                    }
                    catch (Exception ex2)
                    {
                        CError.SetError(ex2.Message);
                        return false;
                    }
                    break;
                case PosType.Before:
                    if (MatchingNodes.Count == 0)
                    {
                        ActionParameters.posType = PosType.After;
                        xmlNode = null;
                        break;
                    }
                    try
                    {
                        xmlNode = BaseNodes[0].SelectSingleNode(CombineXPath(ActionParameters.path, ActionParameters.identifier), NameSpaceManager);
                    }
                    catch (Exception ex)
                    {
                        CError.SetError(ex.Message);
                        return false;
                    }
                    break;
            }
            while (xmlNode != null && xmlNode.ParentNode != BaseNodes[0])
            {
                xmlNode = xmlNode.ParentNode;
            }
            XmlNode newChild = Doc.ImportNode(ActionParameters.node.FirstChild, true);
            switch (ActionParameters.posType)
            {
                case PosType.After:
                case PosType.First:
                    try
                    {
                        BaseNodes[0].InsertAfter(newChild, xmlNode);
                    }
                    catch (Exception ex4)
                    {
                        CError.SetError(ex4.Message);
                        return false;
                    }
                    break;
                case PosType.Before:
                case PosType.Last:
                    try
                    {
                        BaseNodes[0].InsertBefore(newChild, xmlNode);
                    }
                    catch (Exception ex3)
                    {
                        CError.SetError(ex3.Message);
                        return false;
                    }
                    break;
            }
            return true;
        }

        public override void LogStart()
        {
            string text = ".Starting action INSERTNODE";
            text = text + " path:" + ActionParameters.path;
            text = text + " postype:" + ActionParameters.posType;
            if (ActionParameters.identifier != null)
            {
                text = text + " identifier:" + ActionParameters.identifier;
            }
            text = text + " node:" + ActionParameters.node.FirstChild.Name;
            Messages.Add(text);

        }
    }
}
