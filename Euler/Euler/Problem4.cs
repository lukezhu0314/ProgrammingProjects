using System;
using System.Diagnostics;

/* The description of problem 4 reads:
    A palindromic number reads the same both ways. The largest palindroe 
    made from the product of two 2-digit numbers is 9009 = 91 * 99
    Find the largest palindrome made from the product of two 3-digit numbers
    What is the largest prime factor of the number 600851475143?
*/
namespace Euler
{
    class Problem4
    {
        public static int Solve()
        {
            int firstDig;
            int secondDig;
            int thirdDig;

            for (firstDig = 9; firstDig >= 0; firstDig--)
            {
                for (secondDig = 9; secondDig >= 0; secondDig--)
                {
                    for (thirdDig = 9; thirdDig >= 0; thirdDig--)
                    {
                        int num = firstDig + secondDig * 10 + thirdDig * 100 + thirdDig * 1000 + secondDig * 10000 + firstDig * 100000;

                        int divisor = 999;

                        while (((num / divisor) < 1000) && ((divisor * divisor) > num))
                        {
                            if ((num % divisor) == 0)
                            {
                                /*
                                int[] arr = new int[3];
                                arr[0] = num;
                                arr[1] = divisor;
                                arr[2] = num / divisor;
                                return arr;
                                 */
                                
                                return num;
                            }
                            divisor--;
                        }
                    }
                }
            }

            return 69;

        }
    }
}

