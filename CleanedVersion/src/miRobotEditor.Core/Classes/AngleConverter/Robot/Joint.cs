namespace miRobotEditor.Core.Classes.AngleConverter.Robot
{
    public sealed class Joint
    {
       
        public Joint(Joint joint)
        {
            Transform = new TransformationMatrix3D(joint.Transform);
        }

        public Joint(TransformationMatrix3D mat)
        {
            Transform= mat;
        }

        public TransformationMatrix3D Transform
        {
            get; private set; }
    }
}

