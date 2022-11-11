namespace RobotTools.ViewModels.Base
{
    class ToolViewModel : PaneViewModel
    {
        public ToolViewModel(string name)
        {
            Name = name;
            Title = name;
        }

        public string Name
        {
            get;
            private set;
        }


        #region IsVisible

        private bool _isVisible = true;
        public bool IsVisible { get => _isVisible; set => SetProperty(ref _isVisible, value); }

        #endregion


    }
}
