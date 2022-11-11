using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace RobotTools.Core
{
    /// <summary>
    /// FileInfo Wrapper for serialization
    /// </summary>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class FileInfoItem
    {

        private FileInfo _fileInfo;

        public string Extension
        {
            get => _fileInfo.Extension;
            set { }
        }
        public string Name
        {
            get => _fileInfo.Name;
            set { }
        }
        public string FullName
        {
            get => _fileInfo.FullName;
            set { }
        }

        public long Length => _fileInfo.Length;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public FileInfoItem() { }
        public FileInfoItem(string fileName)
        {
            _fileInfo = new FileInfo(fileName);
        }
    }

}
