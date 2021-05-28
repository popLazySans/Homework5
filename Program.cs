using System;

namespace Image_But_not_Image
{
    class Program
    {
        static void Main(string[] args)
        {
            string ImageInputData = Console.ReadLine();
            string ConvolutionInputData = Console.ReadLine();
            string ImageOutputData = Console.ReadLine();
            double[,] ImageArrayData = ReadImageDataFromFile(ImageInputData);
            double[,] ConvolutionArrayData = ReadImageDataFromFile(ConvolutionInputData);

            //WriteNewArray
            double[,] newArray = new double[ImageArrayData.GetLength(0) + ConvolutionArrayData.GetLength(0) - 1, ImageArrayData.GetLength(1) + ConvolutionArrayData.GetLength(1) - 1];
            double[,] fullArray = new double[ImageArrayData.GetLength(0) * 3, ImageArrayData.GetLength(1) * 3];
            while (newArray.GetLength(0) > fullArray.GetLength(0)||newArray.GetLength(1)>fullArray.GetLength(1))
            {
                fullArray = new double [fullArray.GetLength(0) + ImageArrayData.GetLength(0) * 2, fullArray.GetLength(1) + ImageArrayData.GetLength(1) * 2];
            }
      

            //write full array
            for (int a =0;a< fullArray.GetLength(0)/ImageArrayData.GetLength(0); a++) {
                for (int i = 0; i < fullArray.GetLength(1) / ImageArrayData.GetLength(1); i++)
                {
                    for (int j = 0; j < ImageArrayData.GetLength(0); j++)
                    {
                        for (int k = 0; k < ImageArrayData.GetLength(1); k++)
                        {
                            fullArray[j + a*ImageArrayData.GetLength(0), k + i*ImageArrayData.GetLength(1)] = ImageArrayData[j, k];
                        }
                       
                    }
                }
                
            }
            //TestArray(fullArray);
            int dif0 = (fullArray.GetLength(0) - newArray.GetLength(0))/2;
            int dif1 = (fullArray.GetLength(1) - newArray.GetLength(1))/2;
            //write newArray
            for (int i=0;i<newArray.GetLength(0);i++)
            {
                for(int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = fullArray[i + dif0, j + dif1]; 
                }
            }
            //TestArray(newArray);


            double[,] outputArray = new double[ImageArrayData.GetLength(0), ImageArrayData.GetLength(1)];
            for (int u = 0; u < outputArray.GetLength(0); u++)
            {
                for (int v = 0; v < outputArray.GetLength(1); v++)
                {
                    for (int p = 0; p < ConvolutionArrayData.GetLength(0); p++)
                    {
                        for (int q = 0; q < ConvolutionArrayData.GetLength(1); q++)
                        {
                            outputArray[u, v] = outputArray[u, v] + newArray[u + p, v + q] * ConvolutionArrayData[p, q];
                        }
                    }

                }
            }
            //TestArray(outputArray);
            //WriteImageDataToFile(ImageOutputData, outputArray);

            WriteImageDataToFile(ImageOutputData,outputArray);
            
        }
        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }
        static void WriteImageDataToFile(string imageDataFilePath, double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,
                                                imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }

            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }
        static void TestArray(double[,] test)
        {
            for (int a = 0; a < test.GetLength(0); a++)
            {
                for (int b = 0; b < test.GetLength(1); b++)
                {
                    Console.Write(test[a, b]);
                    Console.Write(",");
                }
                Console.WriteLine();
            }
        }

    }
}
