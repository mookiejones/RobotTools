namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBody
    {




        /// <remarks/>
        public ISO15745ProfileProfileBodyDeviceIdentity DeviceIdentity { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyDeviceFunction DeviceFunction { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcess ApplicationProcess { get; set; }
    }

}
