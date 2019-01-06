using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLearning.Library
{
    static public class IdGetter
    {
        static int number_neuron = 0;
        public static int getNumberNeuron()
        {
            return number_neuron++;
        }
    }
}
