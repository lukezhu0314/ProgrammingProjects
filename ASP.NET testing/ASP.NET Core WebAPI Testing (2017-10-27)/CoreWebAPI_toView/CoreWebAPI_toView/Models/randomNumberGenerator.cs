using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI_toView.Models
{
    public class randomNumberGenerator
    {
        public int[,] fakeR = new int[500, 500];
        public int[,] fakeG = new int[500, 500];
        public int[,] fakeB = new int[500, 500];
        
        public randomNumberGenerator()
        {
            Random rnd = new Random();

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    fakeR[i, j] = rnd.Next(0, 255);
                    fakeG[i, j] = rnd.Next(0, 255);
                    fakeB[i, j] = rnd.Next(0, 255);
                }
            }
}
    }
}
