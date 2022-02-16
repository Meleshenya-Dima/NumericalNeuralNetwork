using System;
using System.Collections.Generic;

namespace NumericalNeuralNetwork.MainPage
{
    class InputNeurons
    {
        public InputNeurons(double brightness) => Brightness = brightness;

        public double[] Weights = new double[COUNT_WEIGHTS];
        public double Brightness { get; set; }
        private const int COUNT_WEIGHTS = 16;
        private const double LearningRate = 0.001;

        public double GiveNextBrightness(int numberNextNeuron) => Weights[numberNextNeuron] * Brightness;

        public void Learn(List<double> deltas)
        {
            for (int i = 0; i < COUNT_WEIGHTS; i++)
                Weights[i] = Weights[i] - Brightness * deltas[i] * LearningRate;
        }

        #region Функции SetWeights
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
