using NumericalNeuralNetwork.MainPage;
using NumericalNeuralNetwork.WorkWithNeurons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NumericalNeuralNetwork
{
    public partial class NeuralNetwork : Form
    {
        public NeuralNetwork()
        {
            InitializeComponent();
        }

        #region
        private static readonly string TestPath = @"C:\Users\meles\Desktop\VS\AllProject\NumericalNeuralNetwork\test";
        private static readonly string TrainPath = @"C:\Users\meles\Desktop\VS\AllProject\NumericalNeuralNetwork\train";
        #endregion

        private void TestNeuralNetworkClick(object sender, EventArgs e)
        {
            DirectoryInfo infoTrainFiles = new(TrainPath);
            FileInfo[] filesInfo = infoTrainFiles.GetFiles();
            Random random = new();
            int pocolenie = 0;
            while (pocolenie != 10000)
            {
                int numberImage = random.Next(0, filesInfo.Length);
                string realNumber = filesInfo[numberImage].FullName[^5].ToString();
                Bitmap imageInNumber = (Bitmap)Image.FromFile(filesInfo[numberImage].FullName);
                NumberInImage.Image = imageInNumber;

                // Заполнение первых нейронов
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

                // Вывод числа, о котором думает программа
                double max = 0;
                int index = 0;
                for (int i = 0; i < outputNeurons.Count; i++)
                    if (max < outputNeurons[i].Brightness)
                    {
                        index = i;
                        max = outputNeurons[i].Brightness;
                    }
                //MessageBox.Show(index.ToString());
                if (int.Parse(realNumber) != index)
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
                    pocolenie++;
                    GetSetWeights.SetWeights(inputNeurons, firstHiddenNeurons, secondHiddenNeurons);
                }
                else
                {
                    pocolenie++;
                }
            }
            MessageBox.Show($"Поколение под номером №{pocolenie} закончило обучение");
        }

        private void Coincidence_Click(object sender, EventArgs e)
        {
            DirectoryInfo infoTrainFiles = new(TestPath);
            FileInfo[] filesInfo = infoTrainFiles.GetFiles();
            Random random = new();
            double countCoincidence = 0;
            int pocolenie = 0;
            while (pocolenie != 4000)
            {
                int numberImage = random.Next(0, filesInfo.Length);
                string realNumber = filesInfo[numberImage].FullName[^5].ToString();
                Bitmap imageInNumber = (Bitmap)Image.FromFile(filesInfo[numberImage].FullName);
                NumberInImage.Image = imageInNumber;

                // Заполнение первых нейронов
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
                    outputNeurons.Add(new OutputNeurons(nextBrightness / 16));
                }

                // Вывод числа, о котором думает программа
                double max = 0;
                int index = 0;
                for (int i = 0; i < outputNeurons.Count; i++)
                    if (max < outputNeurons[i].Brightness)
                    {
                        index = i;
                        max = outputNeurons[i].Brightness;
                    }
                if (int.Parse(realNumber) == index)
                {
                    countCoincidence++;
                }
                pocolenie++;
            }
            MessageBox.Show((countCoincidence / pocolenie * 100).ToString() + "% совпадений!");
        }

        private void CheckNeuralNetwork_Click(object sender, EventArgs e)
        {
            DirectoryInfo infoTrainFiles = new(TestPath);
            FileInfo[] filesInfo = infoTrainFiles.GetFiles();
            Random random = new();
            int numberImage = random.Next(0, filesInfo.Length);
            string realNumber = filesInfo[numberImage].FullName[^5].ToString();
            Bitmap imageInNumber = (Bitmap)Image.FromFile(filesInfo[numberImage].FullName);
            NumberInImage.Image = imageInNumber;

            // Заполнение первых нейронов
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
                outputNeurons.Add(new OutputNeurons(nextBrightness / 16));
            }

            // Вывод числа, о котором думает программа
            double max = 0;
            int index = 0;
            for (int i = 0; i < outputNeurons.Count; i++)
                if (max < outputNeurons[i].Brightness)
                {
                    index = i;
                    max = outputNeurons[i].Brightness;
                }
            MessageBox.Show($"Мне кажется, что это число {index}...");
        }
    }
}
