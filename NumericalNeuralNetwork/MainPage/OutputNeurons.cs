using System;
using System.Collections.Generic;

namespace NumericalNeuralNetwork.MainPage
{
    class OutputNeurons
    {
        public OutputNeurons(double brightness) => Brightness = brightness;

        public double[] Weights = new double[COUNT_WEIGHTS];
        public double Brightness { get; set; }
        private const int COUNT_WEIGHTS = 16;

        #region Функция GetException
        private List<double> GetException(int expectedValue)
        {
            List<double> exeptions = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            exeptions[expectedValue] = 1;
            for (int i = 0; i < exeptions.Count; i++)
                exeptions[i] = Brightness - exeptions[i];
            return exeptions;
        }
        #endregion

        #region Функция GetDeltas
        public double GetDeltas(int expectedValue, int numberNeurons)
        {
            List<double> exeptions = GetException(expectedValue);
            return exeptions[numberNeurons] * SigmoidDx(Brightness);
        }
        #endregion

        #region Функция Sigmoid
        public double Sigmoid(double value) => 1.0f / (1.0f + Math.Pow(Math.E, -value));
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
