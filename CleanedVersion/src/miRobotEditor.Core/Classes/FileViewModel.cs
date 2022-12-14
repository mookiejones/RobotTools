using System;

namespace miRobotEditor.Core.Classes
{
    public class FileViewModel:PaneViewModel
    {
        protected FileViewModel(string filepath) : base(filepath)
        {
            
     
        }


        public string FilePath { get; set; }

        
        #region FileName
        /// <summary>
        /// The <see cref="FileName" /> property's name.
        /// </summary>
        private const string FileNamePropertyName = "FileName";

        private string  _filename = String.Empty;

        /// <summary>
        /// Sets and gets the FileName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string  FileName
        {
            get
            {
                return _filename;
            }

            set
            {
                if (_filename == value)
                {
                    return;
                }

                RaisePropertyChanging(FileNamePropertyName);
                _filename = value;
                RaisePropertyChanged(FileNamePropertyName);
            }
        }
        #endregion


        #region IsDirty
        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        private const string IsDirtyPropertyName = "IsDirty";

        private bool _isDirty;

        /// <summary>
        /// Sets and gets the IsDirty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }

            set
            {
                if (_isDirty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsDirtyPropertyName);
                _isDirty = value;
                RaisePropertyChanged(IsDirtyPropertyName);
            }
        }
        #endregion
    }
}
