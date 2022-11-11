namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessExternalTextList
    {



        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Text", IsNullable = false)]
        public ISO15745ProfileProfileBodyApplicationProcessExternalTextListText[] PrimaryLanguage { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessExternalTextListLanguage Language { get; set; }
    }

}
