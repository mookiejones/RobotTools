namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleListVirtualSubmoduleItem
    {









        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleListVirtualSubmoduleItemIOData IOData { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleListVirtualSubmoduleItemRecordDataList RecordDataList { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleListVirtualSubmoduleItemModuleInfo ModuleInfo { get; set; }

        /// <remarks/>
        public object PROFIenergy { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SubmoduleIdentNumber { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool PROFIsafeSupported { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PROFIsafeSupportedSpecified { get; set; }
    }

}
