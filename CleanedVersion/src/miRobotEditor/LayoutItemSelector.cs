using System;
using System.Windows;
using System.Windows.Controls;
using miRobotEditor.EditorControl;
using miRobotEditor.EditorControl.Languages;
using miRobotEditor.UI.Windows;
using miRobotEditor.Views;

namespace miRobotEditor
{
    public class LayoutItemSelector:DataTemplateSelector
    {
        public DataTemplate KukaTemplate { get; set; }
        public DataTemplate FunctionTemplate { get; set; }
        public DataTemplate DocumentTemplate { get; set; }
        public DataTemplate NotesTemplate { get; set; }
        public DataTemplate ObjectBrowserTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Console.WriteLine(@"LayoutItemSelector SelectTemplate : {0}", item.GetType().ToString());

            var view = item as ObjectBrowserView;
            if (view != null)
                return view.ContentTemplate;

            var window = item as NotesWindow;
            if (window != null)
                return window.ContentTemplate;

            var converterWindow = item as AngleConverterWindow;
            if (converterWindow != null)
                return converterWindow.ContentTemplate;

            var functionWindow = item as FunctionWindow;
            if (functionWindow != null)
                return functionWindow.ContentTemplate;


            var kukaModel = item as KukaViewModel;
            if (kukaModel!=null)
                return KukaTemplate;

            var docModel = item as DocumentModel;
            if (docModel!=null)
                return DocumentTemplate;



            return base.SelectTemplate(item, container);
        }
    }
}
