using NumericalNeuralNetwork.MainPage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalNeuralNetwork.WorkWithNeurons
{
    class CreateNeurons
    {
        public static (List<InputNeurons> inputNeurons, List<FirstHiddenNeurons> firstHiddenNeurons, List<SecondHiddenNeurons> secondHiddenNeurons, List<OutputNeurons> outputNeurons) CreateANeuron(Bitmap imageInNumber)
        {
            List<InputNeurons> inputNeurons = GetBrightnessInputNeurons.GetFirstNeurons(imageInNumber);
            string[] weights = GetSetWeights.GetWeights();
            int startWeight = 0;
            for (int i = 0; i < inputNeurons.Count; i++)
                startWeight = inputNeurons[i].SetWeights(weights, startWeight);

            // Заполнение вторых нейронов
            List<FirstHiddenNeurons> firstHiddenNeurons = new List<FirstHiddenNeurons>();
            for (int i = 0; i < 16; i++)
            {
                double nextBrightness = 0;
                for (int j = 0; j < inputNeurons.Count; j++)
                    nextBrightness += inputNeurons[j].GiveNextBrightness(i);
                firstHiddenNeurons.Add(new FirstHiddenNeurons(nextBrightness / 784));
            }
            for (int i = 0; i < firstHiddenNeurons.Count; i++)
                startWeight = firstHiddenNeurons[i].SetWeights(weights, startWeight);

            // Заполнение третьих нейронов
            List<SecondHiddenNeurons> secondHiddenNeurons = new List<SecondHiddenNeurons>();
            for (int i = 0; i < 16; i++)
            {
                double nextBrightness = 0;
                for (int j = 0; j < firstHiddenNeurons.Count; j++)
                    nextBrightness += firstHiddenNeurons[j].GiveNextBrightness(i);
                secondHiddenNeurons.Add(new SecondHiddenNeurons(nextBrightness / 16));
            }
            for (int i = 0; i < secondHiddenNeurons.Count; i++)
                startWeight = secondHiddenNeurons[i].SetWeights(weights, startWeight);

            // Создание последних нейронов
            List<OutputNeurons> outputNeurons = new List<OutputNeurons>();
            for (int i = 0; i < 10; i++)
            {
                double nextBrightness = 0;
                for (int j = 0; j < secondHiddenNeurons.Count; j++)
                    nextBrightness += secondHiddenNeurons[j].GiveNextBrightness(i);
                outputNeurons.Add(new OutputNeurons(nextBrightness / 10));
            }
            return (inputNeurons, firstHiddenNeurons, secondHiddenNeurons, outputNeurons);
        }
        public static int GetNumber(List<OutputNeurons> outputNeurons)
        {
            double max = 0;
            int index = 0;
            for (int i = 0; i < outputNeurons.Count; i++)
                if (max < outputNeurons[i].Brightness)
                {
                    index = i;
                    max = outputNeurons[i].Brightness;
                }
            return index;
        }
    }
}
