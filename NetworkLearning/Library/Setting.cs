using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLearning.Library
{
    public class Setting
    {
        //Одна связь.
        //На одну связь выделяется 1 вес
        public class Relations
        {
            public int myLeftLayer;//слой на котором находится эта связь.
            public double myWeight;
            public int[] leftN;//номер левого нейрона (Пока тут может быть только одно число
            //но потом мб добавлю возможность создавать общие веса для нескольких синапсов
            public int[] rightN;
            public Relations(int LeftLayer,double Weight, int[] NumberLeftNeuron,int[] NumberRightNeuron)
            {
                myWeight = Weight;
                myLeftLayer = LeftLayer;
                leftN = NumberLeftNeuron;
                rightN = NumberRightNeuron;
            }
        }
        public class Biases
        {
            public int index;
            public double value;
            public Biases(int Index, double Value)
            {
                index = Index;
                value = Value;
            }
        }
        public int[] countNeurons;
        public Biases[] bias; // На каких слоях есть нейрон смещения
        public Relations[] relations;// Count Synapses != CountNeurons-1; 1 матрица = 1 синапс ()
        public int[][] fullRelations;
        public Setting(int[] CountNeurons, int[][] FullRelations, Relations[] Relations, Biases[] Bias)
        {
            countNeurons = CountNeurons;
            relations = Relations;
            bias = Bias;
            fullRelations = FullRelations;
        }
    }

}
