using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLearning.Library
{
    public class Networks
    {
        public List<List<Neurons>> neurons;
        List<Synapse> synapses;
        public double Epsilon = 0.7;
        public double Moment = 0.5;
        public double Speed = 0.0009;
        public int typeActivarionF;//0-sigmoid

        public Networks(Setting setting, double epsilon = 0.7,double moment=0.5, double speed=0.0009,
            int typeActivationFunc=0)
        {
            Epsilon = epsilon;
            Moment = moment;
            Speed = speed;
            neurons = new List<List<Neurons>>();
            synapses = new List<Synapse>();
            typeActivarionF = typeActivationFunc;
            initNet(setting);
        }
        public List<double> runNN(List<double> input)
        {
            //Ввод входных параметров
            for(int i=0;i<neurons.ElementAt(0).Count;i++)
            {
                neurons.ElementAt(0).ElementAt(i).value = input.ElementAt(i);
            }

            //Расчет значений
            for(int layer=1; layer < neurons.Count; layer++)
            {
                for(int j=0;j<neurons.ElementAt(layer).Count;j++)
                {
                    neurons.ElementAt(layer).ElementAt(j).culcValue();
                }
            }

            //Вывод параметров
            int last = neurons.Count - 1;
            List<double> output = new List<double>();
            for (int i = 0; i < neurons.ElementAt(last).Count; i++)
            {
                output.Add(neurons.ElementAt(last).ElementAt(i).value);
            }
            return output;
        }

        //Создает НС. Добавлю перегрузку, чтобы можно было задать веса
        //Neuron(тип, функция) //Type: 0-input 1-hidden 2-output 3-bids
        public void initNet(Setting set)
        {
            //Создание нейронов input
            {
                List<Neurons> n = new List<Neurons>();
                for (int j = 0; j < set.countNeurons[0]; j++)
                {
                    n.Add(new Neurons(0, typeActivarionF,0,j));
                }
                neurons.Add(n);
            }
            //Создание нейронов hidden
            for (int i=1;i<set.countNeurons.Length-1;i++)
            {
                List<Neurons> n = new List<Neurons>();
                for (int j=0;j<set.countNeurons[i];j++)
                {
                    n.Add(new Neurons(1,typeActivarionF,i,j));
                }
                neurons.Add(n);
            }
            //Создание нейронов output
            {
                List<Neurons> n = new List<Neurons>();
                for (int j = 0; j < set.countNeurons[set.countNeurons.Length-1]; j++)
                {
                    n.Add(new Neurons(2, typeActivarionF, set.countNeurons.Length - 1,j));
                }
                neurons.Add(n);
            }
            //Создание нейронов bias
            for (int i=0;i<set.bias.Length;i++)
            {
                neurons.ElementAt(set.bias[i]).Add(new Neurons(3, typeActivarionF, set.bias[i],
                    -1));
            }
            //Создание и добавление синапсов
            for(int i=0;i<set.relations.Length;i++)
            {
                Weights w = new Weights(set.relations.ElementAt(i).myWeight);
                Synapse s = new Synapse(w);
                //Добавление левых нейронов
                for(int j=0;j<set.relations.ElementAt(i).leftN.Length;j++)
                {
                    s.leftNeurons.Add(neurons
                        .ElementAt(set.relations.ElementAt(i).myLeftLayer)//ВЫбор слоя
                        .ElementAt(set.relations.ElementAt(i).leftN[j]));//выбор номера нейрона
                }
                //Добавление правых нейронов
                for (int j = 0; j < set.relations.ElementAt(i).rightN.Length; j++)
                {
                    s.rightNeurons.Add(neurons
                        .ElementAt(set.relations.ElementAt(i).myLeftLayer+1)//ВЫбор слоя
                        .ElementAt(set.relations.ElementAt(i).rightN[j]));//выболр номера нейрона
                }
                //Добавление нейронам исходящих синапсов
                for (int j = 0; j < set.relations.ElementAt(i).leftN.Length; j++)
                {
                    neurons
                        .ElementAt(set.relations.ElementAt(i).myLeftLayer)
                        .ElementAt(set.relations.ElementAt(i).leftN[j]).addOutputS(s);
                }
                //Добавление нейронам входящих синапсов
                for (int j = 0; j < set.relations.ElementAt(i).rightN.Length; j++)
                {
                    neurons
                        .ElementAt(set.relations.ElementAt(i).myLeftLayer+1)
                        .ElementAt(set.relations.ElementAt(i).rightN[j]).addInputS(s);
                }
                synapses.Add(s);
            }

        }
    }
}
