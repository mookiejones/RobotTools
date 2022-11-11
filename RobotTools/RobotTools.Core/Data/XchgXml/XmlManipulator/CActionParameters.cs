using System;
using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CActionParameters
    {
        public string type;

        public PosType posType = PosType.Unknown;

        public string path;

        public string location;

        public string identifier;

        public string val;

        public XmlNode node;

        public string defaultNameSpace;

        public bool Set(XmlNode parameterNode)
        {
            XmlNodeList childNodes = parameterNode.SelectSingleNode("type").ChildNodes;
            if (childNodes != null)
            {
                type = childNodes[0].InnerText.ToLower();
            }
            childNodes = parameterNode.SelectSingleNode("params").ChildNodes;
            foreach (XmlNode item in childNodes)
            {
                switch (item.Name.ToLower())
                {
                    case "postype":
                        try
                        {
                            posType = (PosType)Enum.Parse(typeof(PosType), item.InnerText, true);
                        }
                        catch (Exception ex)
                        {
                            CError.SetError(ex.Message);
                            return false;
                        }
                        if (!Enum.IsDefined(typeof(PosType), posType))
                        {
                            return false;
                        }
                        break;
                    case "path":
                        path = item.InnerText;
                        path = path.TrimEnd(' ', '/');
                        path = path.TrimStart(' ', '/', '.');
                        path = "/" + path;
                        break;
                    case "identifier":
                        identifier = item.InnerText;
                        identifier = identifier.TrimStart(null);
                        identifier = identifier.TrimEnd(' ', '/');
                        break;
                    case "location":
                        location = item.InnerText;
                        location = location.Trim(' ', '/');
                        break;
                    case "val":
                        val = item.InnerText;
                        val = val.Trim('"');
                        break;
                    case "node":
                        node = item;
                        break;
                    case "defaultnamespace":
                        defaultNameSpace = item.InnerText.Trim();
                        break;
                    default:
                        CError.SetError("Unknown parameter name" + item.Name + item.BaseURI);
                        return false;
                }
            }
            return true;
        }

        public bool Check()
        {
            return true;
        }
    }
}
