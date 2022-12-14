namespace miRobotEditor.Core.Commands
{
    public abstract class AbstractMenuCommand : AbstractCommand, IMenuCommand
    {
      
		private bool _isenabled = true;
		public virtual bool IsEnabled{get{return _isenabled;}set{_isenabled=value;}}
	
    }
}
