using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class EnvironmentVariablesClass
    {
        public static bool ParametersConfigured { get; set; } = false;
        public static bool IsCalibrationMode { get; set; } = true;
        public static float LastDistanceCoefficient { get; set; } = 0.01f;
        public static float SizeCoefficient { get; set; } = 1;
        public static float TimeCoefficient { get; set; } = 1;
        public static float ClosenessCoefficient { get; set; } = 0.01f;

        public static string[] GetListOfCoefficients()
        {
            return new string[] {
                LastDistanceCoefficient.ToString(),
                SizeCoefficient.ToString(),
                TimeCoefficient.ToString(),
                ClosenessCoefficient.ToString(),
            };
        }

        public static void SetListOfCoefficients(float[] coeff)
        {
            LastDistanceCoefficient = coeff[0];
            SizeCoefficient = coeff[1];
            TimeCoefficient = coeff[2];
            ClosenessCoefficient = coeff[3];

            ParametersConfigured = true;
        }
    }
}
