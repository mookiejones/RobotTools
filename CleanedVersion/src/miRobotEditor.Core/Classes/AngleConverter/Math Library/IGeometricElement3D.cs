using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public interface IGeometricElement3D : IFormattable
    {
        TransformationMatrix3D Position { get; }
    }
}

