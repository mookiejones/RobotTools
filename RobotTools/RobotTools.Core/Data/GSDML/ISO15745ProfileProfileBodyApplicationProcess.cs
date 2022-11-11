namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcess
    {





        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DeviceAccessPointItem", IsNullable = false)]
        public ISO15745ProfileProfileBodyApplicationProcessDeviceAccessPointItem[] DeviceAccessPointList { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ModuleItem", IsNullable = false)]
        public ISO15745ProfileProfileBodyApplicationProcessModuleItem[] ModuleList { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessGraphicsList GraphicsList { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessExternalTextList ExternalTextList { get; set; }
    }

}
