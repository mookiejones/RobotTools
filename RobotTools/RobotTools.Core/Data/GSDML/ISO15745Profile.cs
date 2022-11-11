namespace RobotTools.Core.Data.GSDML
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile", IsNullable = false)]
    public partial class ISO15745Profile
    {



        /// <remarks/>
        public ISO15745ProfileProfileHeader ProfileHeader { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBody ProfileBody { get; set; }
    }

}
