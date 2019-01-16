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
        static public double Sigmoid(double z)
        {
            return (1/(1 + Math.Exp(-1 * SIGMOD_COOF * z)));
        }
        static public double tangsoid(double z)
        {
            return ((Math.Exp(2 * z) - 1) / (Math.Exp(2 * z) + 1));
        }

        //0-sigmoid
        static public double difFunc(double z, int type)
        {
            if (type == 0)
                return z * (1 - z);
            return 0;
        }
    }
}
