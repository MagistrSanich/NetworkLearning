using NetworkLearning.Library;
// Библиотека для JSON 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetworkLearning
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
            Setting set = createSetting();
            Console.WriteLine(setToSting(set));
            Networks network = new Networks(set);
            //Console.WriteLine(JsonConvert.SerializeObject(network.neurons));

            List<double> input = new List<double>() { 0.5, 1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.6 };
            List<double> output = network.runNN(input);
            Console.WriteLine("\n Input: "+JsonConvert.SerializeObject(input)
                +"\nOutput: "+JsonConvert.SerializeObject(output));
            */
            FileStream fstream =
                new FileStream(@"C:\Users\iamse\Downloads\TrainNN\t10k-images.idx3-ubyte",
                FileMode.Open);
            FileStream labStream =
                new FileStream(@"C:\Users\iamse\Downloads\TrainNN\t10k-labels.idx1-ubyte",
                FileMode.Open);
            BinaryReader imageReed = new BinaryReader(fstream);
            BinaryReader labelReed = new BinaryReader(labStream);
            byte b1231233 = imageReed.ReadByte();
            b1231233 = imageReed.ReadByte();
            b1231233 = imageReed.ReadByte();
            int magic = imageReed.ReadInt32();
            int number = imageReed.ReadInt32();
            int rows = imageReed.ReadInt32();
            int colums = imageReed.ReadInt32();
            
            //b1231233 = labelReed.ReadByte();
            int magicLabel = labelReed.ReadInt32();
            int numberLabels= labelReed.ReadInt32();
            Console.WriteLine("Magic: {0}, number: {1}, rows: {2}, colums: {3}, magicL: {4}, nL: {5}"
                , magic, number, rows, colums,magicLabel,numberLabels);

            //Чтение изображения
            for (int k = 0; k < 10; k++)
            {
                int true_answer= labelReed.ReadByte();
                Console.Write("\nImage {0}, true anser: {1}\n", k,true_answer);
                for (int i = 0; i < 28; i++)
                {

                    for (int j = 0; j < 28; j++)
                    {
                        byte b = imageReed.ReadByte();
                        if (b > 0)
                        {
                            b = 1;
                            Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                        }
                        else
                        {
                            b = 0;
                            Console.ResetColor(); // сбрасываем в стандартный
                        }
                        Console.Write(b + " ");
                    }
                    Console.Write("\n");
                    Console.ResetColor(); // сбрасываем в стандартный
                }
            }
            fstream.Close();

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
   
