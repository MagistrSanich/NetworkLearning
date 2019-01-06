using NetworkLearning.Library;
// Библиотека для JSON 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NetworkLearning
{
    public class Program
    {
        static void Main(string[] args)
        {
            Setting set = createSetting();
            Console.WriteLine(setToSting(set));
            Networks network = new Networks(set);
            //Console.WriteLine(JsonConvert.SerializeObject(network.neurons));

            List<double> input = new List<double>() { 0.5, 1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.6};
            List<double> output = network.runNN(input);
            Console.WriteLine("\n Input: "+JsonConvert.SerializeObject(input)
                +"\nOutput: "+JsonConvert.SerializeObject(output));

            Console.Read();
        }
        static Setting createSetting()
        {
            Random random = new Random();
            Setting set = new Setting(
                new int[] { 9, 2, 2 },//Количество нейронов на соответствующих слоях
                new Setting.Relations[] {
                    //Ниже указаны связи. Вх.парам: номер слоя,вес,левые нейроны,правые нейроны
                    new Setting.Relations(0,random.NextDouble(),new int[] { 0,1,2,3},new int[]{0}),
                    new Setting.Relations(0,random.NextDouble(),new int[] { 4,5,6,7},new int[]{1}),
                    new Setting.Relations(0,random.NextDouble(),new int[] { 8},new int[]{0}),
                    new Setting.Relations(0,random.NextDouble(),new int[] { 8},new int[]{1}),
                    new Setting.Relations(1,random.NextDouble(),new int[] { 0},new int[]{0}),
                    new Setting.Relations(1,random.NextDouble(),new int[] { 0},new int[]{1}),
                    new Setting.Relations(1,random.NextDouble(),new int[] { 1},new int[]{0}),
                    new Setting.Relations(1,random.NextDouble(),new int[] { 1},new int[]{1}),
                    new Setting.Relations(1,random.NextDouble(),new int[] { 2},new int[]{0}),
                    new Setting.Relations(1,random.NextDouble(),new int[] { 2},new int[]{1})
                },//Ниже указаны слои на которых есть 1 нейрон смещения 
                new int[] { 1});
            return set;
        }
        static string setToSting(Setting set)
        {
            string serialized = JsonConvert.SerializeObject(set);
            return serialized;
        }
    }
}
   
