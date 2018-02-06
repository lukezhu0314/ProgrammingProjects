using System;

namespace Optional_Parameters
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintData("John");
            PrintData("John", age:35);
        }

        static void PrintData(string firstName, string lastName = null, int age = 0){
            System.Console.WriteLine("{0} {1} is {2} years old", firstName, lastName, age);
        }
        /*
        static void PrintData(string firstName, string lastName, int age){
            System.Console.WriteLine("{0} {1} is {2} years old", firstName, lastName, age);
        }

        static void PrintData(string firstName, string lastName){
            PrintData(firstName, lastName, 0);
        }

        static void PrintData(string firstName){
            PrintData(firstName, null, 0);
        }
        */
    }
}
