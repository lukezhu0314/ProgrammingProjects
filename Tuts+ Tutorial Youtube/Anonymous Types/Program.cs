using System;
using System.Collections.Generic;
using System.Linq;

namespace Anonymous_Types
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person> {
                new Person{FirstName = "John", LastName = "Doe", Age = 20},
                new Person{FirstName = "Jane", LastName = "Doe", Age = 30},
                new Person{FirstName = "Bill", LastName = "Johnson", Age = 15},
                new Person{FirstName = "Sally", LastName = "Johnson", Age = 17},
                new Person{FirstName = "Rupert", LastName = "LastName", Age = 55}
            };

            var result = from p in people 
                        where p.LastName == "Doe"
                        select new { FName = p.FirstName, LName = p.LastName };
            
            //Anonymous type: C# allows programmers to create an object with the new keyword without defining its classes. The implicitly typed variable- var is used to hold the reference of anonymous types. => var myAnonymousType = new {firstProperty = "First"}
            //One of the biggest advantages of using anonymous type with LINQ is that we can choose to only include properties or fields that are of interest to us, instead of having to include every single property defined in the class. Thus saving memory and unnecessary code

            foreach(var p in result)
            {
                Console.WriteLine(p.FName + " " + p.LName);
                //All properties in anonymous type is read-only, => p.FName = "Bob" will give an error
                //Can't pass anonymous type into methods, since you don't know the type of an anonymous type, won't be able to declare parameter type is the function signature
            }
        }
    }

    public class Person{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
        public int MyProperty3 { get; set; }
    }
}
