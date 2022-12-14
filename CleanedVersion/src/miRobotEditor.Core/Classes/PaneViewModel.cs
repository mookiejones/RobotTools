using System;
using GalaSoft.MvvmLight;

namespace miRobotEditor.Core.Classes
{
    public class PaneViewModel:ViewModelBase
    {
// ReSharper disable once UnusedParameter.Local
        protected PaneViewModel(string filename = "")
        {
            
        }

        #region ContentID
        /// <summary>
        /// The <see cref="ContentId" /> property's name.
        /// </summary>
        private const string ContentIdPropertyName = "ContentId";

        private string _contentID = String.Empty;

        /// <summary>
        /// Sets and gets the ContentId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ContentId
        {
            get
            {
                return _contentID;
            }

            set
            {
                if (_contentID == value)
                {
                    return;
                }

                RaisePropertyChanging(ContentIdPropertyName);
                _contentID = value;
                RaisePropertyChanged(ContentIdPropertyName);
            }
        }
        #endregion
  
        #region Title
        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        private const string TitlePropertyName = "Title";

        private string _title = string.Empty;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                {
                    return;
                }

                RaisePropertyChanging(TitlePropertyName);
                _title = value;
                RaisePropertyChanged(TitlePropertyName);
            }
        }
        #endregion

        
        #region Name
        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        private const string NamePropertyName = "Name";

        private string _name = String.Empty;

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        protected string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                RaisePropertyChanging(NamePropertyName);
                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }
        #endregion
    }
}
