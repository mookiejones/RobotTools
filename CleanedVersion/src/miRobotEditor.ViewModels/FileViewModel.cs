using System.IO;
using System.Windows.Media;
using miRobotEditor.Core.Classes;

namespace miRobotEditor.ViewModels
{
  public  class FileViewModel : PaneViewModel
    {
#pragma warning disable 169
        static ImageSourceConverter _isc = new ImageSourceConverter();
#pragma warning restore 169
        public FileViewModel(string filePath)
        {
            FilePath = filePath;
            Title = FileName;
    
        }
      
        public FileViewModel()
        {
            IsDirty = true;
            Title = FileName;
        }
        
        
        #region FilePath
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath == value) return;
                _filePath = value;
                RaisePropertyChanged("FilePath");
                RaisePropertyChanged("FileName");
                RaisePropertyChanged("Title");

                if (File.Exists(_filePath))
                {
                    ContentId = _filePath;
                }
            }
        }
        #endregion


        public string FileName
        {
            get
            {
                if (FilePath == null)
                    return "Noname" + (IsDirty ? "*" : "");

                return Path.GetFileName(FilePath) + (IsDirty ? "*" : "");
            }
        }



        #region IsDirty

        private bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty == value) return;
                _isDirty = value;
                RaisePropertyChanged("IsDirty");
                RaisePropertyChanged("FileName");
            }
        }

        #endregion


    }
 
}
