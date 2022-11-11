namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleListVirtualSubmoduleItemIOData
    {







        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleListVirtualSubmoduleItemIODataInput Input { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleListVirtualSubmoduleItemIODataOutput Output { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte IOPS_Length { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte IOCS_Length { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort F_IO_StructureDescCRC { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool F_IO_StructureDescCRCSpecified { get; set; }
    }

}
