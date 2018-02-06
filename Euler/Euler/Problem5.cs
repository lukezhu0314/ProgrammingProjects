using System;
using System.Diagnostics;

/* The description of problem 5 reads:
    2520 is the smallest number that can be divided by each of the numbers
    from 1 to 10 without any remainder.
    What is the smallest posiitive number that is evenly divisible (divisible with no remainder)
    by all of the numbers from 1 to 20
*/

/* Need to check are even numbers that are divisible by 19, 17, 15, 13, 11, 9, 7
   the even number also has to be a common multiple of 16, since 16 can be factored into 4 2s, the most amount of 2s of all even numbers from 1 - 20
*/

namespace Euler
{
    class Problem5
    {
        public static void Solve()
        {
            Stopwatch clock = Stopwatch.StartNew();
            int i = 16;
            while (((i % 7) != 0) || ((i % 9) != 0) || ((i % 11) != 0) || ((i % 13) != 0) || ((i % 15) != 0) || ((i % 17) != 0) || ((i % 19) != 0))
            {
                i += 16;
            }

            clock.Stop();
            Console.WriteLine("The solution is {0}, and took {1} milliseconds to solve", i, clock.ElapsedMilliseconds);
        }
    }
}

//However the solution doesn't scale well, The following algorithm addresses the issue:
/*
        let's denote the maximum divisor as k, and prime numbers within the range of divisors as pi, where i indicates the ith elemeent
        We need to find the smallest set of primes such that all numbers from 1-20 could be constructed, in the form of
        k = (p1)^(a1)*(p2)^(a2)*(p3)^(a3)*....
        the way to find the ai values could be calculated as follows:
        ai = Math.floor(k/pi), since pi ^ (ai) < k < pi ^ (ai + 1)
*/

/*
        private int[] generatePrimes(int upperLimit){
            List<int> primes = new List<int>();
                bool isPrime;
                int j;
 
                primes.Add(2);
 
                for (int i = 3; i <= upperLimit; i += 2) {
                    j = 0;
                    isPrime = true;
                    while (primes[j]*primes[j] <= i) {
                        if (i % primes[j] == 0) {
                            isPrime = false;
                            break;
                        }
                        j++;
                    }
                    if (isPrime) {
                        primes.Add(i);
                    }
                }
                return primes.ToArray<int>();
            }

            
            //main function
            int divisorMax = 20;
            int[] p = generatePrimes(divisorMax);
            int result = 1;
 
            for (int i = 0; i < p.Length; i++) {
            int a = (int) Math.Floor(Math.Log(divisorMax) / Math.Log(p[i]));
                result = result * ((int)Math.Pow(p[i],a));
            }
*/
