using NumericalNeuralNetwork.ClassHelper;
using NumericalNeuralNetwork.MainPage;
using System.Collections.Generic;
using System.Drawing;

namespace NumericalNeuralNetwork.WorkWithNeurons
{
    class GetBrightnessInputNeurons
    {
        public static List<InputNeurons> GetFirstNeurons(Bitmap OpenToDopictures)
        {
            // Узнаем количество пикселей 
            var GetPixelsColorandPoint = new List<Pixel>(OpenToDopictures.Width * OpenToDopictures.Height);
            // Достаем пиксели
            for (int y = 0; y < OpenToDopictures.Height; y++)
            {
                for (int x = 0; x < OpenToDopictures.Width; x++)
                {
                    GetPixelsColorandPoint.Add(new Pixel()
                    {
                        ColorPixels = OpenToDopictures.GetPixel(x, y),
                        PointPixels = new Point() { X = x, Y = y }
                    });
                }
            }
            List<InputNeurons> inputNeurons = new List<InputNeurons>();
            for (int i = 0; i < GetPixelsColorandPoint.Count; i++)
                inputNeurons.Add(new InputNeurons(GetPixelsColorandPoint[i].ColorPixels.GetBrightness()));
            return inputNeurons;
        }
    }
}
