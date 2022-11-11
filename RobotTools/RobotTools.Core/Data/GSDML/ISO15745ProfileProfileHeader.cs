namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileHeader
    {







        /// <remarks/>
        public string ProfileIdentification { get; set; }

        /// <remarks/>
        public decimal ProfileRevision { get; set; }

        /// <remarks/>
        public string ProfileName { get; set; }

        /// <remarks/>
        public string ProfileSource { get; set; }

        /// <remarks/>
        public string ProfileClassID { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileHeaderISO15745Reference ISO15745Reference { get; set; }
    }

}
