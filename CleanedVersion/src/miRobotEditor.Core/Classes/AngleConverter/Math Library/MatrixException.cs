using System;
using System.ComponentModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Serializable]
    public sealed class MatrixException : Exception
    {
        public MatrixException([Localizable(false)] string message):base(message)
        {

        }
    }
}