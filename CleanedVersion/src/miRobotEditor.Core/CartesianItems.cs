using System.Collections.Generic;
using miRobotEditor.Core.Enums;

namespace miRobotEditor.Core
{
    public sealed class CartesianItems : List<CartesianTypes>
    {
        // Methods
        public CartesianItems()
        {
            var item = new CartesianTypes
                {
                    ValueCartesianEnum = CartesianEnum.ABB_Quaternion,
                    ValueCartesianString = "ABB Quaternion"
                };
            Add(item);
            var types2 = new CartesianTypes
                {
                    ValueCartesianEnum = CartesianEnum.Roll_Pitch_Yaw,
                    ValueCartesianString = "Roll-Pitch-Yaw"
                };
            Add(types2);
            var types3 = new CartesianTypes
                {
                    ValueCartesianEnum = CartesianEnum.Axis_Angle,
                    ValueCartesianString = "Axis Angle"
                };
            Add(types3);
            var types4 = new CartesianTypes
                {
                    ValueCartesianEnum = CartesianEnum.Kuka_ABC,
                    ValueCartesianString = "Kuka ABC"
                };
            Add(types4);
            var types5 = new CartesianTypes
                {
                    ValueCartesianEnum = CartesianEnum.Euler_ZYZ,
                    ValueCartesianString = "Euler ZYZ"
                };
            Add(types5);
            var types6 = new CartesianTypes
                {
                    ValueCartesianEnum = CartesianEnum.Alpha_Beta_Gamma,
                    ValueCartesianString = "Alpha-Beta-Gamma"
                };
            Add(types6);
        }
    }
}