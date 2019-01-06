using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLearning.Library
{
    public class Neurons
    {
        public int typeNeuron;//0-input 1-hidden 2-output 3-bids
        public int typeActivation;//0-sigmoid
        List<Synapse> inputSynapses;
        List<Synapse> outputSynapses;
        //List<double> outputWeights;
        public double value { get; set; }
        public double delta { get; set; }
        public int myLayer { get; set; }
        public int myNumber { get; set; }
        public int idN { get; }
        //Methods
        //Рассчет значения
        public void culcValue()
        {
            if(typeNeuron==3)
            {
                value = 1;
                return;
            }
            double summAll=0;
            foreach(Synapse synapse in inputSynapses)
            {
                double summ = 0;
                foreach(Neurons n in synapse.leftNeurons)
                {
                    summ += n.value;
                }
                summAll += summ * synapse.weight.weight;
            }
            value = CulcNN.Sigmoid(summAll);
        }
        public void culcDelta(double trueAnswer = 0)
        {
            if (typeNeuron == 0 || typeNeuron == 3) return;
        }
        public void addInputS(Synapse synapse)
        {
            inputSynapses.Add(synapse);
        }
        public void addOutputS(Synapse synapse)
        {
            outputSynapses.Add(synapse);
        }

        public Neurons(int TypeNeuron,int TypeActivation, int Layer,int NumberNeuron)
        {
            inputSynapses = new List<Synapse>();
            outputSynapses = new List<Synapse>();
            typeNeuron = TypeNeuron;
            typeActivation = TypeActivation;
            myLayer = Layer;
            myNumber = NumberNeuron;
            idN = IdGetter.getNumberNeuron();
        }
    }
}
