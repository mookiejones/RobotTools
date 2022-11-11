namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyDeviceIdentity
    {





        /// <remarks/>
        public ISO15745ProfileProfileBodyDeviceIdentityInfoText InfoText { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyDeviceIdentityVendorName VendorName { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DeviceID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VendorID { get; set; }
    }

}
