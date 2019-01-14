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
        public int activFunc;//0-sigmoid
        List<Synapse> inputSynapses;
        List<Synapse> outputSynapses;
        //List<double> outputWeights;
        public double value { get; set; }
        public double delta = 0;
        public double z { get; set; }
        public int myLayer { get; set; }
        public int myNumber { get; set; }
        public int idN { get; }
        //Methods
        //Расчет значения
        public void culcValue()
        {
            if(typeNeuron==3)
            {
                value = 1;
                return;
            }
            double summ=0;
            foreach(Synapse synapse in inputSynapses)
            {
                summ += synapse.leftNeuron.value * synapse.weight.w;
            }
            z = summ;
            value = CulcNN.Sigmoid(summ);
        }
        public void culcDelta()
        {
            if (typeNeuron == 0) return;
            double summ = 0;
            foreach (Synapse synapse in outputSynapses)
            {
                summ += synapse.rightNeuron.delta * synapse.weight.w;
            }
            delta = summ * CulcNN.difFunc(z, activFunc);
        }
        public void culcWeight(double speed,int m)
        {
            if(typeNeuron==3)
            {

            }
            foreach (Synapse synapse in inputSynapses)
            {
                double dw = speed / m * (delta * synapse.leftNeuron.value);
                synapse.weight.updateWeight(dw);
            }
        }
        public void addInputS(Synapse synapse)
        {
            inputSynapses.Add(synapse);
        }
        public void addOutputS(Synapse synapse)
        {
            outputSynapses.Add(synapse);
        }
        public void setDelta(double d)
        {
            this.delta = d;
        }
        public Neurons(int TypeNeuron,int TypeActivation, int Layer,int NumberNeuron)
        {
            inputSynapses = new List<Synapse>();
            outputSynapses = new List<Synapse>();
            typeNeuron = TypeNeuron;
            activFunc = TypeActivation;
            myLayer = Layer;
            myNumber = NumberNeuron;
            idN = IdGetter.getNumberNeuron();
        }
    }
}
