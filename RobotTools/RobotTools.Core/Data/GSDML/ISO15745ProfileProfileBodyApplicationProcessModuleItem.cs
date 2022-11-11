﻿namespace RobotTools.Core.Data.GSDML
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.profibus.com/GSDML/2003/11/DeviceProfile")]
    public partial class ISO15745ProfileProfileBodyApplicationProcessModuleItem
    {





        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessModuleItemModuleInfo ModuleInfo { get; set; }

        /// <remarks/>
        public ISO15745ProfileProfileBodyApplicationProcessModuleItemVirtualSubmoduleList VirtualSubmoduleList { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ModuleIdentNumber { get; set; }
    }

}
