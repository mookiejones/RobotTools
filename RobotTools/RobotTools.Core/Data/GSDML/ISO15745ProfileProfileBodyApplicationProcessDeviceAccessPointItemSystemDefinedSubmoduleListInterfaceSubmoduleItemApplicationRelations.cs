namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemSystemDefinedSubmoduleListInterfaceSubmoduleItemApplicationRelations
    {






        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemSystemDefinedSubmoduleListInterfaceSubmoduleItemApplicationRelationsTimingProperties TimingProperties { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NumberOfAdditionalInputCR { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NumberOfAdditionalMulticastProviderCR { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NumberOfAdditionalOutputCR { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NumberOfMulticastConsumerCR { get; set; }
    }

}
