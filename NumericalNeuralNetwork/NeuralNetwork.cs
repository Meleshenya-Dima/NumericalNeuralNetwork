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
                List<InputNeurons> inputNeurons = new List<InputNeurons>();
                List<FirstHiddenNeurons> firstHiddenNeurons = new List<FirstHiddenNeurons>();
                List<SecondHiddenNeurons> secondHiddenNeurons = new List<SecondHiddenNeurons>();
                List<OutputNeurons> outputNeurons = new List<OutputNeurons>();
                (inputNeurons, firstHiddenNeurons, secondHiddenNeurons, outputNeurons) = CreateNeurons.CreateANeuron(imageInNumber);

                // Вывод числа, о котором думает программа
                int index = CreateNeurons.GetNumber(outputNeurons);
                if (int.Parse(realNumber) != index)
                    LearnNeuron.LearnNeurons(inputNeurons, firstHiddenNeurons, secondHiddenNeurons, outputNeurons, realNumber);
                pocolenie++;
            }
            MessageBox.Show($"Поколение под номером №{pocolenie} закончило обучение");
        }

        private void Coincidence_Click(object sender, EventArgs e)
        {
            DirectoryInfo infoTrainFiles = new(TrainPath);
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
                List<InputNeurons> inputNeurons = new List<InputNeurons>();
                List<FirstHiddenNeurons> firstHiddenNeurons = new List<FirstHiddenNeurons>();
                List<SecondHiddenNeurons> secondHiddenNeurons = new List<SecondHiddenNeurons>();
                List<OutputNeurons> outputNeurons = new List<OutputNeurons>();
                (inputNeurons, firstHiddenNeurons, secondHiddenNeurons, outputNeurons ) = CreateNeurons.CreateANeuron(imageInNumber);

                // Вывод числа, о котором думает программа
                int index = CreateNeurons.GetNumber(outputNeurons);
                if (int.Parse(realNumber) == index)
                    countCoincidence++;
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
            List<InputNeurons> inputNeurons = new List<InputNeurons>();
            List<FirstHiddenNeurons> firstHiddenNeurons = new List<FirstHiddenNeurons>();
            List<SecondHiddenNeurons> secondHiddenNeurons = new List<SecondHiddenNeurons>();
            List<OutputNeurons> outputNeurons = new List<OutputNeurons>();
            (inputNeurons, firstHiddenNeurons, secondHiddenNeurons, outputNeurons) = CreateNeurons.CreateANeuron(imageInNumber);

            // Вывод числа, о котором думает программа
            MessageBox.Show($"Мне кажется, что это число {CreateNeurons.GetNumber(outputNeurons)}...");
        
        }

        private void Information_Click(object sender, EventArgs e) => MessageBox.Show("Разработчик: Мелещеня Дмитрий Иванович. Учебная группа: 2119.", "Информация о разработчике", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
