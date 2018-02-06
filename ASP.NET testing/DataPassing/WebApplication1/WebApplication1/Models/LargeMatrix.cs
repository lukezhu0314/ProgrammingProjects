using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LargeMatrix
    {
        public static int[,] colorRGB_one = new int[500, 250];
        public static int[,] colorRGB_two = new int[500, 250];
        public static int[,] colorRGB_three = new int[500, 250];
        public static double[,] probabilityMatrix_one = new double[500, 250];
        public static double[,] probabilityMatrix_two = new double[500, 250];

        public static void DataInput()
        {
            Random rnd = new Random();

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 250; j++)
                {
                    colorRGB_one[i, j] = rnd.Next(255);
                    colorRGB_two[i, j] = rnd.Next(255);
                    colorRGB_three[i, j] = rnd.Next(255);
                    probabilityMatrix_one[i, j] = Math.Round(rnd.NextDouble(),3);
                    probabilityMatrix_two[i, j] = Math.Round(rnd.NextDouble(),3);
                }
            }
        }
        

        /*
        public static int[,,] colorRGB = new int[500, 500, 3];
        public static double[,,] probabilityMatrix = new double[500, 500, 2];
        public static void DataInput()
        {
            Random rnd = new Random();

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    colorRGB[i, j, 0] = rnd.Next(255);
                    colorRGB[i, j, 1] = rnd.Next(255);
                    colorRGB[i, j, 2] = rnd.Next(255);
                    probabilityMatrix[i, j, 0] = Math.Round(rnd.NextDouble(),3);
                    probabilityMatrix[i, j, 1] = Math.Round(rnd.NextDouble(),3);
                }
            }
        }
        */
    }
}
