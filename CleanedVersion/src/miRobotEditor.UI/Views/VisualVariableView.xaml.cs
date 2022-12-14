using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace miRobotEditor.UI.Controls
{
    /// <summary>
    /// Interaction logic for VisualVariableView.xaml
    /// </summary>
    public partial class VisualVariableView : UserControl
    {
        public VisualVariableView()
        {
            InitializeComponent();
        }

        private void ToolTip_Opening(object sender, ToolTipEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
