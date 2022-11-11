namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItem
    {

















        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemModuleInfo ModuleInfo { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemIOConfigData IOConfigData { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ModuleItemRef", IsNullable = false)]
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemModuleItemRef[] UseableModules { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemVirtualSubmoduleList VirtualSubmoduleList { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemSystemDefinedSubmoduleList SystemDefinedSubmoduleList { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemGraphics Graphics { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte FixedInSlots { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PhysicalSlots { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort MinDeviceInterval { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ModuleIdentNumber { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DNS_CompatibleName { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImplementationType { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ObjectUUID_LocalIndex { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool DeviceAccessSupported { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AddressAssignment { get; set; }
    }

}
