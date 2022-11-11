using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public abstract class CActionBase
    {
        protected CActionParameters ActionParameters;

        protected XmlNode ActionNode;

        protected XmlDocument Doc;

        protected XmlNamespaceManager NameSpaceManager;

        protected XmlNodeList BaseNodes;

        protected ArrayList MatchingNodes = new ArrayList();

        protected ArrayList LocNodes = new ArrayList();

        public abstract bool DoIt();

        public abstract void LogStart();

        public List<string> Messages { get; set; } = new List<string>();
        public CActionBase(ref CActionParameters actionParameters, XmlDocument doc)
        {
            Doc = doc;
            ActionParameters = actionParameters;
        }

        public bool Init()
        {
            LogStart();
            if (ActionParameters.defaultNameSpace != null)
            {
                NameSpaceManager = new XmlNamespaceManager(Doc.NameTable);
                NameSpaceManager.AddNamespace("nsprf", ActionParameters.defaultNameSpace);
            }
            else if (Doc.DocumentElement.NamespaceURI.Length > 0)
            {
                NameSpaceManager = new XmlNamespaceManager(Doc.NameTable);
                NameSpaceManager.AddNamespace("nsprf", Doc.DocumentElement.NamespaceURI);
            }
            else
            {
                NameSpaceManager = null;
            }
            if (ActionParameters.path != null)
            {
                try
                {
                    BaseNodes = Doc.DocumentElement.SelectNodes(ActionParameters.path, NameSpaceManager);
                }
                catch (Exception ex)
                {
                    CError.SetError(ex.Message);
                    return false;
                }
                if (BaseNodes == null || BaseNodes.Count == 0)
                {
                    if (ActionParameters.type == "removenode")
                    {
                        return true;
                    }
                    CError.SetError("Base path not existing: " + ActionParameters.path);
                    return false;
                }
            }
            if (ActionParameters.identifier == null)
            {
                foreach (XmlNode baseNode in BaseNodes)
                {
                    MatchingNodes.Add(baseNode);
                }
            }
            else
            {
                string text = CombineXPath(ActionParameters.path, ActionParameters.identifier);
                foreach (XmlNode baseNode2 in BaseNodes)
                {
                    XmlNodeList xmlNodeList = null;
                    try
                    {
                        xmlNodeList = baseNode2.SelectNodes(text, NameSpaceManager);
                    }
                    catch (Exception ex2)
                    {
                        CError.SetError(ex2.Message);
                        return false;
                    }
                    foreach (XmlNode item in xmlNodeList)
                    {
                        for (XmlNode xmlNode3 = item; xmlNode3 != null; xmlNode3 = xmlNode3.ParentNode)
                        {
                            if (xmlNode3 == baseNode2)
                            {
                                MatchingNodes.Add(xmlNode3);
                                break;
                            }
                        }
                    }
                }
                if (MatchingNodes.Count == 0)
                {
                    string line = $".No matching nodes found for path: {text}"  ;
                    Messages.Add(line);

                }
            }
            return true;
        }

        protected void FindLocNodes(bool reportError)
        {
            if (ActionParameters.location == null)
            {
                return;
            }
            foreach (XmlNode matchingNode in MatchingNodes)
            {
                XmlNode xmlNode2 = matchingNode.SelectSingleNode(ActionParameters.location, NameSpaceManager);
                if (xmlNode2 != null)
                {
                    LocNodes.Add(xmlNode2);
                }
                else if (reportError)
                {
                    CError.SetError("Locator not existing: " + ActionParameters.location + matchingNode.Name);
                }
            }
        }

        protected void RemoveMatchingNodes()
        {
            foreach (XmlNode matchingNode in MatchingNodes)
            {
                matchingNode.ParentNode.RemoveChild(matchingNode);
            }
        }

        protected void RemoveLocNodes()
        {
            foreach (XmlNode locNode in LocNodes)
            {
                locNode.ParentNode.RemoveChild(locNode);
            }
        }

        protected string CombineXPath(string str1, string str2)
        {
            str1.Trim();
            str2.Trim();
            str1.TrimEnd('/');
            str2.TrimStart('/');
            if (str1.Length == 0)
            {
                return str2;
            }
            if (str2.Length == 0)
            {
                return str1;
            }
            if (str2.StartsWith("["))
            {
                return str1 + str2;
            }
            return str1 + "/" + str2;
        }
    }
}
