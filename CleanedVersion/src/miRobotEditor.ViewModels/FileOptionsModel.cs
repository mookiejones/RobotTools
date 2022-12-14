using System.Windows;

namespace miRobotEditor.ViewModels
{
    public sealed class FileOptionsModel:DependencyObject
    {



        public bool ShowFullName
        {
            get { return (bool)GetValue(ShowFullNameProperty); }
            set { SetValue(ShowFullNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowFullName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowFullNameProperty =
            DependencyProperty.Register("ShowFullName", typeof(bool), typeof(FileOptionsModel), new PropertyMetadata(true));


    }
}
