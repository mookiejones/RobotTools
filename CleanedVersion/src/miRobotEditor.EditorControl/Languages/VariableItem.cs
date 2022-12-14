using System.Windows;
using System.Windows.Media.Imaging;

namespace miRobotEditor.EditorControl.Languages
{
    public class VariableItem : DependencyObject
    {
        public BitmapImage Icon { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Value { get; set; }
    }
}