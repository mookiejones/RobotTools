using System.ComponentModel;

namespace miRobotEditor.EditorControl
{
    public class GlobalOptions : IOptions
    {
        private GlobalOptions()
        {
            FlyoutOpacity = .85;
        }

        [Localizable(false)]
        public string Title { get { return "Global Options"; } }
        private static GlobalOptions _instance;
        public static GlobalOptions Instance
        {
            get { return _instance ?? (_instance = new GlobalOptions()); }
            set { _instance = value; }
        }

        #region Flyout Options
        [DefaultValue(0.75)]
        public double FlyoutOpacity { get; set; }

        #endregion
    }
}