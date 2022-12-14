using System.Windows;

namespace miRobotEditor.Core.Classes
{
    public abstract class SerializeBase:DependencyObject
    {

        public abstract string SerializeFileName { get; set; }

        public void Serialize() { }
        public void Deserialize() { }

    }
}