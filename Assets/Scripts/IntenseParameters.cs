using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class IntenseParameters
    {
        public static bool ParametersSet { get; set; } = false;
        public static float LastDistanceCoefficient { get; set; }
        public static float SizeCoefficient { get; set; }
        public static float TimeCoefficient { get; set; }
        public static float ClosenessCoefficient { get; set; }

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

            ParametersSet = true;
        }
    }
}
