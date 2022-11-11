using System;

namespace RobotTools.ViewModels
{
    internal class PaneViewModel : ViewModelBase
    {
        public PaneViewModel()
        { }


        #region Title

        private string _title = null;
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        #endregion

        public virtual Uri IconSource
        {
            get;

            protected set;
        }

        #region ContentId

        private string _contentId = null;
        public string ContentId { get => _contentId; set => SetProperty(ref _contentId, value); }

        #endregion

        #region IsSelected

        private bool _isSelected = false;
        public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }

        #endregion

        #region IsActive

        private bool _isActive = false;
        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
      

        #endregion


    }
}
