using RobotTools.Docking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTools.ViewModels
{
    class FileStatsViewModel : Base.ToolViewModel
    {
        public FileStatsViewModel()
          : base("File Stats")
        {
            MainViewModel.This.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
            ContentId = ToolContentId;
        }

        public const string ToolContentId = "FileStatsTool";

        void OnActiveDocumentChanged(object sender, EventArgs e)
        {
            FileSize = 0;
            LastModified = DateTime.MinValue;

            if (MainViewModel.This.ActiveDocument != null)
            {
                FileViewModel f = MainViewModel.This.ActiveDocument as FileViewModel;

                if (f != null)
                {
                    if (f.FilePath != null && File.Exists(f.FilePath))
                    {
                        var fi = new FileInfo(f.FilePath);
                        FileSize = fi.Length;
                        LastModified = fi.LastWriteTime;
                    }

                }
            }
        }

        #region FileSize

        private long _fileSize;
        public long FileSize
        {
            get { return _fileSize; }
            set
            {
                if (_fileSize != value)
                {
                    _fileSize = value;
                    OnPropertyChanged("FileSize");
                }
            }
        }

        #endregion

        #region LastModified

        private DateTime _lastModified;
        public DateTime LastModified
        {
            get { return _lastModified; }
            set
            {
                if (_lastModified != value)
                {
                    _lastModified = value;
                    OnPropertyChanged("LastModified");
                }
            }
        }

        #endregion

        public override Uri IconSource
        {
            get
            {
                return new Uri("pack://application:,,,/RobotTools;component/Images/property-blue.png", UriKind.RelativeOrAbsolute);
            }
        }
    }
}
