namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemSystemDefinedSubmoduleListInterfaceSubmoduleItem
    {










        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemSystemDefinedSubmoduleListInterfaceSubmoduleItemApplicationRelations ApplicationRelations { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort SubslotNumber { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SubmoduleIdentNumber { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TextId { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SupportedRT_Classes { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsochroneModeSupported { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SupportedProtocols { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SupportedMibs { get; set; }
    }

}
