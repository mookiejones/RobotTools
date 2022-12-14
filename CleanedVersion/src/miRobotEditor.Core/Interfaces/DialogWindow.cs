using System.Windows.Input;

namespace miRobotEditor.Core.Interfaces
{
    class DialogWindow
    {
// ReSharper disable UnusedMember.Local
        string Title { get; set; }
        string Description { get; set; }
        ICommand OkCommand { get; set; }
        ICommand CancelCommand { get; set; }
        // ReSharper restore UnusedMember.Local

    }
}
