using System.Windows.Controls;
using System.Windows;
using RobotTools.ViewModels;
using RobotTools.ViewModels.Base;

namespace RobotTools
{
    class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle
        {
            get;
            set;
        }

        public Style FileStyle
        {
            get;
            set;
        }

        public Style StartPageStyle
        {
            get;
            set;
        }

        public Style RecentFilesStyle
        {
            get;
            set;
        }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is ToolViewModel)
                return ToolStyle;

            if (item is FileViewModel)
                return FileStyle;

            if (item is StartPageViewModel)
                return StartPageStyle;

            if (item is RecentFilesViewModel)
                return RecentFilesStyle;

            return base.SelectStyle(item, container);
        }
    }
}
