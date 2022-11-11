using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RobotTools.Core
{
    //[Serializable]
    //[DesignerCategoryAttribute("code")]
    //[XmlTypeAttribute(AnonymousType = true)]
    //[XmlRoot(Namespace = "", IsNullable = false)]

    public class TreeNode : IXmlSerializable
    {
        public string Header { get; set; }

        public FileInfoItem Tag { get; set; }
        public string ToolTip { get; set; }
        public List<TreeNode> Items { get; set; } = new List<TreeNode>();

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetType().ToString())
            {
                Header = reader["Header"];
                ToolTip = reader["ToolTip"];


            }
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }


    public class TreeNodes : List<TreeNode>
    {
        public XmlSchema GetSchema() => null;


    }

}
