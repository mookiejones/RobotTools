using System.Windows;

namespace miRobotEditor.ViewModels
{
    public sealed class GlobalOptionsModel:DependencyObject
    {


        public FileOptionsModel FileOptions
        {
            get { return (FileOptionsModel)GetValue(FileOptionsProperty); }
            set { SetValue(FileOptionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileOptions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileOptionsProperty =
            DependencyProperty.Register("FileOptions", typeof(FileOptionsModel), typeof(GlobalOptionsModel), new PropertyMetadata(new FileOptionsModel()));

         }
}
