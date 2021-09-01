using System.Collections.Generic;
using COPOL.BLL.Models;

namespace FunctionalTests.Common
{
    public static class ParametersHelper
    {
        public static Parameters GetParameters()
        {
            return new Parameters
            {
                Cgs = 20,
                Cds = 5,
                Cgd = 30,
                Ls = 1,
                Ld = 20,
                Gm = 1000,
                Vds0 = 3,
                Ids0 = 500,
                N = 3,
                Step = 1,
                LoopP = 0,
                Frequences = new List<float>
                {
                    0.1f,
                    0.5f
                }
            };
        }
    }
}