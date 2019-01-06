using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLearning.Library
{
    static public class CulcNN
    {
        const double SIGMOD_COOF = 1.0;
        const double SPEED = 0.00004;
        static public double Sigmoid(double z)
        {
            return (Math.Pow(1 + Math.Exp(-1 * SIGMOD_COOF * z), -1));
        }
        static public double tangsoid(double z)
        {
            return ((Math.Exp(2 * z) - 1) / (Math.Exp(2 * z) + 1));
        }
    }
}
