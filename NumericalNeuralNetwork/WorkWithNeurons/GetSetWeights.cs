using NumericalNeuralNetwork.MainPage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumericalNeuralNetwork.WorkWithNeurons
{
    class GetSetWeights
    {
        private static string writePath = @"C:\Users\meles\Desktop\VS\AllProject\NumericalNeuralNetwork\NumericalNeuralNetwork\Weights.txt";

        public static string[] GetWeights()
        {
            string[] Weights;
            using StreamReader stream = new StreamReader(writePath);
            Weights = stream.ReadToEnd().Split(" ");
            return Weights;
        }

        public static void SetWeights(List<InputNeurons> inputNeurons, List<FirstHiddenNeurons> firstHiddenNeurons, List<SecondHiddenNeurons> secondHiddenNeurons)
        {
            List<double> weights = new List<double>();
            for (int i = 0; i < inputNeurons.Count; i++)
                for (int j = 0; j < 16; j++)
                    weights.Add(inputNeurons[i].Weights[j]);
            for (int i = 0; i < firstHiddenNeurons.Count; i++)
                for (int j = 0; j < 16; j++)
                    weights.Add(firstHiddenNeurons[i].Weights[j]);
            for (int i = 0; i < secondHiddenNeurons.Count; i++)
                for (int j = 0; j < 10; j++)
                    weights.Add(secondHiddenNeurons[i].Weights[j]);
            using StreamWriter stream = new StreamWriter(writePath);
            for (int i = 0; i < weights.Count; i++)
            {
                stream.Write(weights[i]);
                if (i != 12959)
                    stream.Write(" ");
            }
        }
    }
}
