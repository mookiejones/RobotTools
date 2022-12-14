using System.Windows;

namespace miRobotEditor.ViewModels
{
    public class CustomToolTipModel:DependencyObject
    {



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CustomToolTipModel), new PropertyMetadata(""));



        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(CustomToolTipModel), new PropertyMetadata(""));





        public string Additional
        {
            get { return (string)GetValue(AdditionalProperty); }
            set { SetValue(AdditionalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Additional.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdditionalProperty =
            DependencyProperty.Register("Additional", typeof(string), typeof(CustomToolTipModel), new PropertyMetadata(""));

        

       
    }
}
