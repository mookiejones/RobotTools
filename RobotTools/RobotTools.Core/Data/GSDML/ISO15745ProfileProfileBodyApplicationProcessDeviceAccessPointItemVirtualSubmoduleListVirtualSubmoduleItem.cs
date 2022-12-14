namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemVirtualSubmoduleListVirtualSubmoduleItem
    {






        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemVirtualSubmoduleListVirtualSubmoduleItemIOData IOData { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemVirtualSubmoduleListVirtualSubmoduleItemModuleInfo ModuleInfo { get; set; }

        /// <remarks/>
        public object PROFIenergy { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SubmoduleIdentNumber { get; set; }
    }

}
