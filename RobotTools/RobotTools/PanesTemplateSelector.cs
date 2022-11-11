using System.Windows.Controls;
using System.Windows;
using AvalonDock.Layout;
using RobotTools.ViewModels;

namespace RobotTools
{
    class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {

        }

        public DataTemplate FileViewTemplate
        {
            get;
            set;
        }

        public DataTemplate StartPageViewTemplate
        {
            get;
            set;
        }

        public DataTemplate RecentFilesViewTemplate
        {
            get;
            set;
        }

        public DataTemplate FileStatsViewTemplate
        {
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is FileViewModel)
                return FileViewTemplate;

            if (item is StartPageViewModel)
                return StartPageViewTemplate;

            if (item is FileStatsViewModel)
                return FileStatsViewTemplate;

            if (item is RecentFilesViewModel)
                return RecentFilesViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
