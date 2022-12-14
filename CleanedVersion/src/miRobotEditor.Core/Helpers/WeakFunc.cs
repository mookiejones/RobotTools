using System;
using System.Reflection;
using miRobotEditor.Core.Interfaces;

namespace miRobotEditor.Core.Helpers
{

    public class WeakFunc<TResult>
    {
        private Func<TResult> _staticFunc;
        protected MethodInfo Method
        {
            get;
            set;
        }
        public bool IsStatic
        {
            get
            {
                return _staticFunc != null;
            }
        }
        public virtual string MethodName
        {
            get
            {
                if (_staticFunc != null)
                {
                    return _staticFunc.Method.Name;
                }
                return Method.Name;
            }
        }
        protected WeakReference FuncReference
        {
            get;
            set;
        }
        protected WeakReference Reference
        {
            get;
            set;
        }
        public virtual bool IsAlive
        {
            get
            {
                if (_staticFunc == null && Reference == null)
                {
                    return false;
                }
                if (_staticFunc != null)
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
        protected object FuncTarget
        {
            get
            {
                if (FuncReference == null)
                {
                    return null;
                }
                return FuncReference.Target;
            }
        }
        protected WeakFunc()
        {
        }
        public WeakFunc(Func<TResult> func)
            : this(func.Target, func)
        {
        }
        public WeakFunc(object target, Func<TResult> func)
        {
            if (func.Method.IsStatic)
            {
                _staticFunc = func;
                if (target != null)
                {
                    Reference = new WeakReference(target);
                }
                return;
            }
            Method = func.Method;
            FuncReference = new WeakReference(func.Target);
            Reference = new WeakReference(target);
        }
        public TResult Execute()
        {
            if (_staticFunc != null)
            {
                return _staticFunc();
            }
            if (IsAlive && Method != null && FuncReference != null)
            {
                return (TResult)Method.Invoke(FuncTarget, null);
            }
            return default(TResult);
        }
        public void MarkForDeletion()
        {
            Reference = null;
            FuncReference = null;
            Method = null;
            _staticFunc = null;
        }
    }
    public sealed class WeakFunc<T, TResult> : WeakFunc<TResult>, IExecuteWithObjectAndResult
    {
        private Func<T, TResult> _staticFunc;
        public override string MethodName
        {
            get
            {
                if (_staticFunc != null)
                {
                    return _staticFunc.Method.Name;
                }
                return Method.Name;
            }
        }
        public override bool IsAlive
        {
            get
            {
                if (_staticFunc == null && Reference == null)
                {
                    return false;
                }
                if (_staticFunc != null)
                {
                    return Reference == null || Reference.IsAlive;
                }
                return Reference.IsAlive;
            }
        }
        public WeakFunc(Func<T, TResult> func)
            : this(func.Target, func)
        {
        }

        private WeakFunc(object target, Func<T, TResult> func)
        {
            if (func.Method.IsStatic)
            {
                _staticFunc = func;
                if (target != null)
                {
                    Reference = new WeakReference(target);
                }
                return;
            }
            Method = func.Method;
            FuncReference = new WeakReference(func.Target);
            Reference = new WeakReference(target);
        }
        public new TResult Execute()
        {
            return Execute(default(T));
        }
        public TResult Execute(T parameter)
        {
            if (_staticFunc != null)
            {
                return _staticFunc(parameter);
            }
            if (IsAlive && Method != null && FuncReference != null)
            {
                return (TResult)Method.Invoke(FuncTarget, new object[]
                {
                    parameter
                });
            }
            return default(TResult);
        }
        public object ExecuteWithObject(object parameter)
        {
            var parameter2 = (T)parameter;
            return Execute(parameter2);
        }
        public new void MarkForDeletion()
        {
            _staticFunc = null;
            base.MarkForDeletion();
        }
    }
}
