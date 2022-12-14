using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Serializable]
    public sealed class MatrixNullReference : NullReferenceException
    {
        public MatrixNullReference(string message)
            : base(message)
        {
        }
        public MatrixNullReference()
        { }
    }
}