using System;
using System.Collections.Generic;

namespace NumericalNeuralNetwork.MainPage
{
    class SecondHiddenNeurons
    {
        public SecondHiddenNeurons(double brightness) => Brightness = brightness;

        public double[] Weights = new double[COUNT_WEIGHTS];
        public double Brightness { get; set; }
        private const int COUNT_WEIGHTS = 10;
        private const double LearningRate = 0.00001;

        public double GiveNextBrightness(int numberNextNeuron)
        {
            return Weights[numberNextNeuron] * Brightness;
        }

        #region Функция Learn
        public void Learn(List<double> deltas)
        {
            for (int i = 0; i < COUNT_WEIGHTS; i++)
                Weights[i] = Weights[i] - Brightness * deltas[i] * LearningRate;
        }
        #endregion

        #region Функция GetException
        private List<double> GetException(List<SecondHiddenNeurons> secondHiddenNeurons, List<double> deltas)
        {
            List<double> exeptions = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < secondHiddenNeurons.Count; i++)
                for (int j = 0; j < deltas.Count; j++)
                    exeptions[i] += secondHiddenNeurons[i].Weights[j] * deltas[j];
            return exeptions;
        }
        #endregion

        #region Функция GetDeltas
        public double GetDeltas(List<SecondHiddenNeurons> secondHiddenNeurons, List<double> delta, int numberNeuron)
        {
            List<double> exeptions = GetException(secondHiddenNeurons, delta);
            return exeptions[numberNeuron] * SigmoidDx(Brightness);
        }
        #endregion

        #region Функции Set Weights
        public int SetWeights(string[] weights, int startWeight)
        {
            for (int i = 0; i < COUNT_WEIGHTS; i++)
            {
                Weights[i] = double.Parse(weights[startWeight]);
                startWeight++;
            }
            return startWeight;
        }
        #endregion

        #region Функция Sigmoid
        public double Sigmoid(double value)
        {
            var result = 1.0f / (1.0f + Math.Pow(Math.E, -value));
            return result;
        }
        #endregion

        #region Функция SigmoidDx()
        public double SigmoidDx(double x)
        {
            var sigmoid = Sigmoid(x);
            var result = sigmoid / (1 - sigmoid);
            return result;
        }
        #endregion

    }
}
