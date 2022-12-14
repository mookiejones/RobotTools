using System;
using System.Reflection;
using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.Core.Helpers
{
    public class WeakAction
    {
        private Action _staticAction;
        protected MethodInfo Method
        {
            get;
            set;
        }
        public virtual string MethodName
        {
            get
            {
                if (_staticAction != null)
                {
                    return _staticAction.Method.Name;
                }
                return Method.Name;
            }
        }
        protected WeakReference ActionReference
        {
            get;
            set;
        }
        protected WeakReference Reference
        {
            get;
            set;
        }
        public bool IsStatic
        {
            get
            {
                return _staticAction != null;
            }
        }
        public virtual bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null)
                {
                    return false;
                }
                if (_staticAction != null)
                {
                    return Reference == null || Reference.IsAlive;
                }
                return Reference.IsAlive;
            }
        }
        public object Target
        {
            get
            {
                if (Reference == null)
                {
                    return null;
                }
                return Reference.Target;
            }
        }
        protected object ActionTarget
        {
            get
            {
                if (ActionReference == null)
                {
                    return null;
                }
                return ActionReference.Target;
            }
        }
        protected WeakAction()
        {
        }
        public WeakAction(Action action)
            : this(action.Target, action)
        {
        }
        public WeakAction(object target, Action action)
        {
            if (action.Method.IsStatic)
            {
                _staticAction = action;
                if (target != null)
                {
                    Reference = new WeakReference(target);
                }
                return;
            }
            Method = action.Method;
            ActionReference = new WeakReference(action.Target);
            Reference = new WeakReference(target);
        }
        public void Execute()
        {
            if (_staticAction != null)
            {
                _staticAction();
                return;
            }
            object actionTarget = ActionTarget;
            if (IsAlive && Method != null && ActionReference != null && actionTarget != null)
            {
                Method.Invoke(ActionTarget, null);
            }
        }

        protected void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            Method = null;
            _staticAction = null;
        }
    }
    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        private Action<T> _staticAction;
        public override string MethodName
        {
            get
            {
                if (_staticAction != null)
                {
                    return _staticAction.Method.Name;
                }
                return Method.Name;
            }
        }
        public override bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null)
                {
                    return false;
                }
                if (_staticAction != null)
                {
                    return Reference == null || Reference.IsAlive;
                }
                return Reference.IsAlive;
            }
        }
        public WeakAction(Action<T> action)
            : this(action.Target, action)
        {
        }
        public WeakAction(object target, Action<T> action)
        {
            if (action.Method.IsStatic)
            {
                _staticAction = action;
                if (target != null)
                {
                    Reference = new WeakReference(target);
                }
                return;
            }
            Method = action.Method;
            ActionReference = new WeakReference(action.Target);
            Reference = new WeakReference(target);
        }
        public new void Execute()
        {
            Execute(default(T));
        }
        public void Execute(T parameter)
        {
            if (_staticAction != null)
            {
                _staticAction(parameter);
                return;
            }
            if (IsAlive && Method != null && ActionReference != null)
            {
                Method.Invoke(ActionTarget, new object[]
				{
					parameter
				});
            }
        }
        public void ExecuteWithObject(object parameter)
        {
            T parameter2 = (T)((object)parameter);
            Execute(parameter2);
        }
        public new void MarkForDeletion()
        {
            _staticAction = null;
            base.MarkForDeletion();
        }
    }

}
