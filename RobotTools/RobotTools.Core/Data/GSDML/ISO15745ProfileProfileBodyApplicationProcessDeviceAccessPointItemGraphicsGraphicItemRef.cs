namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItemGraphicsGraphicItemRef
    {



        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte GraphicItemTarget { get; set; }
    }

}
