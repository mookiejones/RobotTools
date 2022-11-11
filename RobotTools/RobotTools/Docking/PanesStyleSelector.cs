using ICSharpCode.AvalonEdit.Document;
using System.Windows;
using System.Windows.Controls;

namespace RobotTools.Docking
{
    public class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle { get; set; }

        public Style FileStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ToolModel) return ToolStyle;
            if (item is IDocument) return FileStyle;

            //TODO Still Need to add file explorer
            return base.SelectStyle(item, container);
        }
    }
}
