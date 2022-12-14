using System;
using System.Windows.Input;

namespace miRobotEditor.Core.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        object _owner;

        /// <summary>
        /// Returns the owner of the command.
        /// </summary>
        public virtual object Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
                OnOwnerChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public abstract void Run();


        protected virtual void OnOwnerChanged(EventArgs e)
        {
            if (OwnerChanged != null)
            {
                OwnerChanged(this, e);
            }
        }
        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, e);
            }
        }
        public event EventHandler OwnerChanged;
    	
        public event EventHandler CanExecuteChanged;
    	
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    	
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}