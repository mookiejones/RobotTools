using System.Windows;

namespace miRobotEditor.UI.Windows
{
    /// <summary>
    /// Interaction logic for GotoWindow.xaml
    /// </summary>
    public partial class GotoWindow : Window
    {
        /// <summary>
        /// Goto Line
        /// </summary>
        /// <param name="linecount">count of lines in active editor</param>
        public GotoWindow(int linecount = 0)
        {
            InitializeComponent();
            LineCount = linecount;
        }

        public GotoWindow()
        {
            InitializeComponent();
        }
        private void ExecuteCloseCommand(string parameter)
        {

            switch (parameter)
            {
                case "_OK":
                    DialogResult = true;
                    break;
                case "_Cancel":
                    DialogResult = false;
                    break;
            }
        }
        public int LineCount
        {
            get { return (int)GetValue(LineCountProperty); }
            set { SetValue(LineCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineCountProperty =
            DependencyProperty.Register("LineCount", typeof(int), typeof(GotoWindow), new PropertyMetadata(0));

    }
}
