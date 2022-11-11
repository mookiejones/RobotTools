namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileHeaderISO15745Reference
    {




        /// <remarks/>
        public byte ISO15745Part { get; set; }

        /// <remarks/>
        public byte ISO15745Edition { get; set; }

        /// <remarks/>
        public string ProfileTechnology { get; set; }
    }

}
