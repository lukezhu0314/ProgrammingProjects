using System;
using System.Diagnostics;

namespace PacMan
{
    class OldProbability
    {
        static void Main(string[] args)
        {
            int factor = 2;
            int canvasRow = 1024;
            int centerRow = canvasRow / 2;
            int canvasColumn = 1024;
            int centerColumn = canvasColumn / 2;
            double[,,] probabilityMatrix = new double[canvasRow, canvasColumn, 2];
            double[,,] rgbCode = new double[canvasRow, canvasColumn, 3];

            TimeConstant.Prompt();

            Stopwatch clock = new Stopwatch();
            clock.Start();

            for (int i = 0; i < canvasRow- 1; i++)
            {
                for (int j = 0; j < canvasColumn - 1; j++)
                {
                    if (Math.Pow(i - centerRow,2) + Math.Pow(j - centerColumn,2) <= Math.Pow(TimeConstant.t_PSC_CSC, 2) * factor)
                    {
                        double x = Math.Pow((Math.Pow(i - centerRow, 2) + Math.Pow(j - centerColumn,2)), 0.5) / factor;
                        double z = Math.Pow((Math.Pow(i - (centerRow + TimeConstant.t_PSC_CSC), 2) + Math.Pow(j - centerColumn, 2)), 0.5) / factor;

                        ProbabilityCalculation.Calculation(x, z, out double PmRS01_MS, out double PmRS01_DnS);
                        probabilityMatrix[i, j, 0] = PmRS01_MS;
                        probabilityMatrix[i, j, 1] = PmRS01_DnS;

                        ColorCode.RGBColor(PmRS01_MS, PmRS01_DnS, out int R, out int G, out int B);
                        rgbCode[i, j, 0] = R;
                        rgbCode[i, j, 1] = G;
                        rgbCode[i, j, 2] = B;
                    }
                }
            }
            
            Console.WriteLine("the solution took {0} ms", clock.ElapsedMilliseconds);
            Console.WriteLine(rgbCode[512, 512, 1]);
            Console.ReadKey();
        }
    }
}
