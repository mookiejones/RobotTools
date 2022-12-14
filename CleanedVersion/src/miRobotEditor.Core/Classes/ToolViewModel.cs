namespace miRobotEditor.Core.Classes
{

    public class ToolViewModel:PaneViewModel
    {
        protected ToolViewModel(string name)
        {
            Name = name;
            Title = name;
        }
        
        #region IsActive
        /// <summary>
        /// The <see cref="IsActive" /> property's name.
        /// </summary>
        private const string IsActivePropertyName = "IsActive";

        private bool _isActive;

        /// <summary>
        /// Sets and gets the IsActive property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                if (_isActive == value)
                {
                    return;
                }

                RaisePropertyChanging(IsActivePropertyName);
                _isActive = value;
                RaisePropertyChanged(IsActivePropertyName);
            }
        }
        #endregion

        // Sizes for External Tools
        public int Height { get; set; }
        public int Width { get; set; }

        public DefaultToolPane DefaultPane = DefaultToolPane.None;

        protected ToolViewModel() : base(null)
        {

        }
    }

    public enum DefaultToolPane
    {
        None,
        Bottom,
        Left,
        Right,
        Top
    }
}
