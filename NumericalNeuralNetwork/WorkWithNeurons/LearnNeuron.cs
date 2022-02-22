using NumericalNeuralNetwork.MainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalNeuralNetwork.WorkWithNeurons
{
    class LearnNeuron
    {
        public static void LearnNeurons(List<InputNeurons> inputNeurons, List<FirstHiddenNeurons> firstHiddenNeurons, List<SecondHiddenNeurons> secondHiddenNeurons, List<OutputNeurons> outputNeurons, string realNumber)
        {
            // Получение дельт от выходного слоя
            List<double> deltasOutputWeights = new List<double>();
            for (int i = 0; i < outputNeurons.Count; i++)
                deltasOutputWeights.Add(outputNeurons[i].GetDeltas(int.Parse(realNumber), i));

            // Изменения весов от второго невидимого слоя 
            for (int i = 0; i < secondHiddenNeurons.Count; i++)
                secondHiddenNeurons[i].Learn(deltasOutputWeights);

            // Получения дельт от второго невидимого слоя
            List<double> deltasSecondHiddenNeuronsWeights = new List<double>();
            for (int i = 0; i < secondHiddenNeurons.Count; i++)
                deltasSecondHiddenNeuronsWeights.Add(secondHiddenNeurons[i].GetDeltas(secondHiddenNeurons, deltasOutputWeights, i));

            // Изменения весов от первого невидимого слоя 
            for (int i = 0; i < firstHiddenNeurons.Count; i++)
                firstHiddenNeurons[i].Learn(deltasSecondHiddenNeuronsWeights);

            // Получения дельт от первого невидимого слоя
            List<double> deltasFirstHiddenNeuronsWeights = new List<double>();
            for (int i = 0; i < firstHiddenNeurons.Count; i++)
                deltasFirstHiddenNeuronsWeights.Add(firstHiddenNeurons[i].GetDeltas(firstHiddenNeurons, deltasSecondHiddenNeuronsWeights, i));

            // Изменения весов от входного слоя 
            for (int i = 0; i < inputNeurons.Count; i++)
                inputNeurons[i].Learn(deltasFirstHiddenNeuronsWeights);
            GetSetWeights.SetWeights(inputNeurons, firstHiddenNeurons, secondHiddenNeurons);
        }
    }
}
