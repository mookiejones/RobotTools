using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace miRobotEditor.Core.Classes
{
    public sealed class MenuTemplateSelector:DataTemplateSelector
    {

        public DataTemplate KUKATemplate { get; set; }
        public DataTemplate FanucTemplate { get; set; }
        public DataTemplate NachiTemplate { get; set; }
        public DataTemplate ABBTemplate { get; set; }

        

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element != null && item != null && item is Task)
            {
                // if (taskitem.Priority == 1)
              //     return
              //         element.FindResource("importantTaskTemplate") as DataTemplate;
              // else
              //     return
              //         element.FindResource("myTaskTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
