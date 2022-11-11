using RobotTools.ViewModels;
using System.Windows.Media;

namespace RobotTools.Docking
{
    public class PaneModel :ViewModelBase
    {
        // ReSharper disable once UnusedParameter.Local
        protected PaneModel(string filename = "")
        {

        }


        #region ContentID


        private string _contentID = string.Empty;

        /// <summary>
        /// Sets and gets the ContentId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ContentId { get; set; }

        #endregion
        public string Title { get; set; }
        protected string Name { get; set; }



      
    }
}
