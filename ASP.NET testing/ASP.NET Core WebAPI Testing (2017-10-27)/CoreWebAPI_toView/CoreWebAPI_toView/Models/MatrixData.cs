using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI_toView.Models
{
    public class MatrixData
    {
        public int[,] RgbDataR { get; set; }
        public int[,] RgbDataG { get; set; }
        public int[,] RgbDataB { get; set; }
        public double[,] ProbData { get; set; }
    }
}
