using System;

namespace Polymorphism_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Tesla tesla = new Tesla();
            tesla.Engine();
            Console.WriteLine(tesla.TyreSize());
            Console.ReadLine();
        }
    }

    public class Car
    {
        public int PowerOfEngine { get; set; }
        public int RPM { get; set; }
        public int NumberOfCylinders { get; set; }

        public virtual void Engine()
        {
            PowerOfEngine = 70;
            RPM = 8000;
            NumberOfCylinders = 4;
            Console.WriteLine($"{PowerOfEngine}, {RPM}, {NumberOfCylinders}");
        }
        public virtual int TyreSize()
        {
            return 8;
        }
    }
    public class Tesla:Car
    {
        public override void Engine()
        {
            base.Engine();
            PowerOfEngine = 80;
            RPM = 9000;
            NumberOfCylinders = 7;
            Console.WriteLine($"After the function overrid, we have {PowerOfEngine}, {RPM}, {NumberOfCylinders}");
            Console.ReadLine();
        }
        public override int TyreSize()
        {
            return 4;
        }
    }
}
