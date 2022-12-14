using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public static class RandomNumber
    {      
        public static double Between(double lowerBound, double upperBound)
        {
            var rand = new Random();
            return ((rand.NextDouble() * (upperBound - lowerBound)) + lowerBound);
        }

        public static double Get(double nominalValue, double range)
        {
            var rand = new Random();
            return ((((rand.NextDouble() * range) * 2.0) + nominalValue) - range);
        }
    }
}

