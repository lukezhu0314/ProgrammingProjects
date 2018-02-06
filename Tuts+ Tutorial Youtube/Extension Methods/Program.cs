using System;
using Extension_Methods;

namespace Extension_Methods
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person{Name = "John", Age = 23};
            var person2 = new Person{Name = "Derek", Age = 51};
            person.SayHello(person2);  
            
            //It seems like when directly calling the function SayHello under the Extension class, it would ignore the this keyword
            Extension.SayHello(person, person2);  
        }
    }

    public static class Extension{
        public static void SayHello(this Person p, Person p2){
            Console.WriteLine("{0} says hello to {1}", p.Name, p2.Name);
        }
    }

}
