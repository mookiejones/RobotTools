namespace miRobotEditor.Core.Interfaces
{
    public interface IExecuteWithObject
    {
        object Target
        {
            get;
        }
        void ExecuteWithObject(object parameter);
        void MarkForDeletion();
    }
}
