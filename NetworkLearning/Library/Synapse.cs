using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLearning.Library
{
    public class Weights
    {
        public double w;
        public double lastDW;
        public double currDW;
        public Weights(double Weight)
        {
            w = Weight;
            lastDW = 0;
            currDW = 0;
        }
        public void updateWeight(double dw)
        {
            lastDW = currDW;
            currDW = dw;
            w -= currDW;
        }
        public void setWeight(double weight)
        {
            w = weight;
        }
        Weights() { }
    }
    public class Synapse
    {
        public Neurons leftNeuron;
        public Neurons rightNeuron;

        public Weights weight;

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
