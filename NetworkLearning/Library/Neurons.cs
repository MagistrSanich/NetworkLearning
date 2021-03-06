﻿using System;
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
        //Methods
        //Расчет значения
        public void culcValue()
        {
            if(typeNeuron==3)
            {
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
            if (typeNeuron == 0 || typeNeuron == 3) return;
            double summ = 0;
            foreach (Synapse synapse in outputSynapses)
            {
                summ += synapse.rightNeuron.delta * synapse.weight.w;
            }
            delta = summ * CulcNN.difFunc(z, activFunc);
        }
        public void culcWeight(double speed,int m)
        {
            foreach (Synapse synapse in outputSynapses)
            {

                double dw = speed / m * (synapse.leftNeuron.value * synapse.rightNeuron.delta);
                synapse.weight.updateWeight(dw);
            }
        }
        public void setWeights()
        {
            foreach(Synapse s in outputSynapses)
            {
                s.weight.setWeight( 1 );
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
        public void setValue(double v)
        {
            this.value = v;
        }
        public Neurons(int TypeNeuron,int TypeActivation, int Layer, double Value=0)
        {
            inputSynapses = new List<Synapse>();
            outputSynapses = new List<Synapse>();
            typeNeuron = TypeNeuron;
            activFunc = TypeActivation;
            myLayer = Layer;
            value = Value;
        }
    }
}
