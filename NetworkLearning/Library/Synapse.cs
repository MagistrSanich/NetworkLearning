using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLearning.Library
{
    public class Weights
    {
        public double weight;
        public Weights(double Weight)
        {
            weight = Weight;
        }
        Weights() { }
    }
    public class Synapse
    {
        public Neurons leftNeuron;
        public Neurons rightNeuron;

        public Weights weight;
        public double dw;
        public double lastDW;

        public int typeSynapse { get; }//0-обычный 1-делит вес с другими

        public Synapse( Weights Weight)
        {
            weight = Weight;
            //leftNeurons = new Neurons();
            //rightNeurons = new List<Neurons>();
        }

        //public void learnLesson()
    }
}
