using System;

namespace VolumeWebService.VolumeCalculator
{
    public interface ICalculator
    {
        double CalculateVolumeCylinder(decimal height, decimal radius);
        double CalculateVolumeCone(decimal height, decimal radius);
    }
    
    public class Calculator : ICalculator
    {
        public double CalculateVolumeCylinder(decimal height, decimal radius)
        {
            return (double)((decimal)Math.PI * radius * radius * height);
        }

        public double CalculateVolumeCone(decimal height, decimal radius)
        {
            return (double)((decimal)1.0 / (decimal)3.0 * (decimal)Math.PI * radius * radius * height);
        }
    }
}