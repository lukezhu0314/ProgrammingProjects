using System;
using System.Diagnostics;

namespace Euler
{
    class Problem1
    {
        public void Solve()
        {
            Stopwatch clock = Stopwatch.StartNew();
            int result = SumDivisbleBy(3, 999) + SumDivisbleBy(5, 999) - SumDivisbleBy(15, 999);
            clock.Stop();

            Console.WriteLine(result);
            Console.WriteLine("Solution took {0} ms", clock.ElapsedMilliseconds);
        }

        private int SumDivisbleBy(int n, int p)
        {
            return n * (p / n) * ((p / n) + 1) / 2;
        }
    }
}

class Results
{
    static void Main(string[] args)
    {
        //Euler.Problem1 Prob1 = new Euler.Problem1();
        //Prob1.Solve();

        Euler.Problem5.Solve();
        Console.ReadLine();
    }
}

