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
        public double epsilon;
        public double moment;
        public double speed;
        public int activFunc;//0-sigmoid

        public Networks(Setting setting, double epsilon = 0.7, double moment = 0.5, double speed = 0.0009,
            int typeActivationFunc = 0)
        {
            this.epsilon = epsilon;
            this.moment = moment;
            this.speed = speed;
            neurons = new List<List<Neurons>>();
            synapses = new List<Synapse>();
            activFunc = typeActivationFunc;
            initNet(setting);
        }
        public List<double> runNN(List<double> input)
        {
            //Ввод входных параметров
            for (int i = 0; i < neurons.ElementAt(0).Count; i++)
            {
                neurons.ElementAt(0).ElementAt(i).value = input.ElementAt(i);
            }

            //Расчет значений
            for (int layer = 1; layer < neurons.Count; layer++)
            {
                for (int j = 0; j < neurons.ElementAt(layer).Count; j++)
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
        public void learnNN(List<double> input,List<double> trueAnswer, int m)
        {
            runNN(input);

            //Расчет дельт выходного слоя
            int L = neurons.Count - 1;//Номер последнего слоя
            int countL = neurons.Last().Count;//Количество н. на последнем слое
            for (int ner = 0; ner < countL; ner++)
            {
                double a = neurons.ElementAt(countL).ElementAt(ner).value - trueAnswer.ElementAt(ner);
                double fn = CulcNN.difFunc(neurons.ElementAt(countL).ElementAt(ner).z, activFunc);
                neurons.ElementAt(L).ElementAt(ner).setDelta(a * fn);
            }

            //Расчет дельт. Идем с предпоследного слоя до 1 (потому что 0 входной)
            for (int lay = L - 1; lay >= 1; lay--)
            {
                for (int ner = 0; ner < neurons.ElementAt(lay).Count; ner++)
                {
                    neurons.ElementAt(lay).ElementAt(ner).culcDelta();
                }
            }

            //Меняем веса
            for (int lay = L; lay >= 1; lay--)
            {
                for (int ner = 0; ner < neurons.ElementAt(lay).Count; ner++)
                {
                    neurons.ElementAt(lay).ElementAt(ner).culcWeight(speed, m);
                }
            }
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
                    n.Add(new Neurons(0, activFunc, 0, j));
                }
                neurons.Add(n);
            }
            //Создание нейронов hidden
            for (int i = 1; i < set.countNeurons.Length - 1; i++)
            {
                List<Neurons> n = new List<Neurons>();
                for (int j = 0; j < set.countNeurons[i]; j++)
                {
                    n.Add(new Neurons(1, activFunc, i, j));
                }
                neurons.Add(n);
            }
            //Создание нейронов output
            {
                List<Neurons> n = new List<Neurons>();
                for (int j = 0; j < set.countNeurons[set.countNeurons.Length - 1]; j++)
                {
                    n.Add(new Neurons(2, activFunc, set.countNeurons.Length - 1, j));
                }
                neurons.Add(n);
            }
            //Создание нейронов bias
            for (int i = 0; i < set.bias.Length; i++)
            {
                neurons.ElementAt(set.bias[i]).Add(new Neurons(3, activFunc, set.bias[i],
                    -1));
            }
            //Создание и добавление синапсов между полносвязными слоями
            for(int i=0;i<set.fullRelations.Length;i++)
            {
                //Проходимся по каждому нейрону левого слоя и добавляем к ним синапс 
                //каждого правого нейрона
                int neuronsLayer = set.fullRelations.ElementAt(i).ElementAt(0);
                Random random = new Random();
                for (int ner=0;ner<neurons.ElementAt(neuronsLayer).Count;ner++)
                {
                    for(int r=0;r<neurons.ElementAt(neuronsLayer+1).Count;r++)
                    {
                        Weights w = new Weights(0.5 + random.NextDouble() / 4);
                        Synapse s = new Synapse(w);
                        //Добавление нейронов в синапс
                        s.leftNeuron = neurons.ElementAt(neuronsLayer).ElementAt(ner);
                        s.rightNeuron = neurons.ElementAt(neuronsLayer+1).ElementAt(r);
                        //Закинуть синапс нейронам
                        neurons.ElementAt(neuronsLayer).ElementAt(ner).addOutputS(s);
                        neurons.ElementAt(neuronsLayer + 1).ElementAt(r).addInputS(s);
                        synapses.Add(s);
                    }
                }
            }
            //Создание и добавление отдельных синапсов
            for (int i = 0; i < set.relations.Length; i++)
            {
                Weights w = new Weights(set.relations.ElementAt(i).myWeight);
                Synapse s = new Synapse(w);

                //Добавление левых нейронов
                s.leftNeuron = neurons
                    .ElementAt(set.relations.ElementAt(i).myLeftLayer)//ВЫбор слоя
                    .ElementAt(set.relations.ElementAt(i).leftN[0]);//выбор номера нейрона (всегда 0)
                                                                    //Добавление правых нейронов
                s.rightNeuron = neurons
                    .ElementAt(set.relations.ElementAt(i).myLeftLayer + 1)//ВЫбор слоя
                    .ElementAt(set.relations.ElementAt(i).rightN[0]);//выболр номера нейрона
                                                                     //Добавление нейронам исходящих синапсов
                neurons
                    .ElementAt(set.relations.ElementAt(i).myLeftLayer)
                    .ElementAt(set.relations.ElementAt(i).leftN[0]).addOutputS(s);
                //Добавление нейронам входящих синапсов
                neurons
                    .ElementAt(set.relations.ElementAt(i).myLeftLayer + 1)
                    .ElementAt(set.relations.ElementAt(i).rightN[0]).addInputS(s);
                synapses.Add(s);
            }

        }
    }
}
