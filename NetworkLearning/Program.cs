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

            Setting set = createSetting();
            Console.WriteLine(setToSting(set));
            Networks network = new Networks(set);
            //Console.WriteLine(JsonConvert.SerializeObject(network.neurons));
            List<List<double>> tests = new List<List<double>>()
            {
                new List<double>() { 0,0 },
                new List<double>() { 0,1 },
                new List<double>() { 1,0 },
                new List<double>() { 1,1 }

            };
            List<List<double>> testAnsw = new List<List<double>>()
            {
                new List<double>() { 0 },
                new List<double>() { 1 },
                new List<double>() { 1 },
                new List<double>() { 0 }

            };
            Console.Write("\nOutput: ");
            for (int i = 0; i < tests.Count; i++)
            {
                List<double> output = network.runNN(tests[i]);
                Console.WriteLine(JsonConvert.SerializeObject(output));
            }
            //Console.WriteLine("\n"+JsonConvert.SerializeObject(network.neurons));
            const int epohs = 20000; // Эпохи
            int m = tests.Count;//Итерации 
            for (int e = 0; e < epohs; e++)
            {
                for (int i = 0; i < m; i++)
                    network.learnNN(tests[i], testAnsw[i], m);
                //network.learnNN(tests[3], testAnsw[3], m);
            }

            Console.Write("\nOutput: ");
            /*List<double> output1 = network.runNN(tests[3]);
            Console.WriteLine(JsonConvert.SerializeObject(output1));*/
            for (int i = 0; i < tests.Count; i++)
            {
                List<double> output = network.runNN(tests[i]);
                Console.WriteLine(JsonConvert.SerializeObject(output));
            }

            Console.Read();
            /*
             //-----------------------------------Считывание изображений ------------------------------
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

            Console.Read();*/
        }
        static Setting createSetting()
        {
            Random random = new Random();
            Setting set = new Setting(
                new int[] { 2, 4, 1 },//Количество нейронов на соответствующих слоях
                                      //Полносвязные слои
                new int[][]
                {
                    new int[]{0,1},//Нейроны левого слоя полностью связаны с н. правого слоя
                    new int[]{1,2}
                }
                ,
                new Setting.Relations[] {
                    //Ниже указаны связи. Вх.парам: номер слоя,вес,левые нейроны,правые нейроны
                },
                true //Использовать смещения
                );
            return set;
        }
        static string setToSting(Setting set)
        {
            string serialized = JsonConvert.SerializeObject(set);
            return serialized;
        }
    }
}
   
