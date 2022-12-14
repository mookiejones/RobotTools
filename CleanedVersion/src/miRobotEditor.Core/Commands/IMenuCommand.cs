using System.Windows.Input;

namespace miRobotEditor.Core.Commands
{
    public interface IMenuCommand : ICommand
    {
        bool IsEnabled
        {
            get;
            set;
        }
    }
}