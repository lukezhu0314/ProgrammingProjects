using System;
using System.Diagnostics;

/* The description of problem 3 reads:
    The prime factors of 13195 are 5,7,13 and 29
    What is the largest prime factor of the number 600851475143?
*/
namespace Euler
{
    class Problem3
    {
        public static void Solve()
        {   
            Stopwatch clock = Stopwatch.StartNew();
            long numm = 600851475143;
            long largestFactor = 1;
            
            while (true)
            {
                int i = 2;
                long nummOld = numm;
                while (i < (numm / i))

                {
                    if (numm % i == 0)
                    {
                        if (i > largestFactor)
                        {
                            largestFactor = i;
                        }
                        numm = numm / i;
                        break;
                    }
                    i++;
                }

                if (nummOld == numm)
                {
                    if (numm > largestFactor)
                    {
                        largestFactor = numm;
                    }
                    break;
                }
            }
            
            clock.Stop();
            Console.WriteLine(largestFactor);
            Console.WriteLine("The solution took {0} ms", clock.ElapsedMilliseconds);
        }
    }
}
