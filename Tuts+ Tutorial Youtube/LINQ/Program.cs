using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //LINQ stands for Language Integrated Query
            //String is a collection, a collection(array) of characters

            /*
            var sample = "I enjoy writing uber-software in C#";

            var result = from c in sample.ToLower()
                            where c == 'a' || c == 'e' || c == 'i' || c =='o' || c == 'u'
                            orderby c
                            group c by c;
            
            foreach(var item in result){
                System.Console.WriteLine(item);
                System.Console.WriteLine("{0} - {1}", item.Key, item.Count());
            }
            */

            var people = new List<Person>{
                new Person{FirstName = "John", LastName = "Doe", Age = 25},
                new Person{FirstName = "Jane", LastName = "Doe", Age = 26},
                new Person{FirstName = "John", LastName = "Williams", Age = 40},
                new Person{FirstName = "Samantha", LastName = "Williams", Age = 35},
                new Person{FirstName = "Bob", LastName = "Walters", Age = 12},
            };

            var Group = from p in people
                        where p.Age < 60 
                        orderby p.LastName descending
                        group p by p.LastName;

            foreach(var LastName in Group){
                System.Console.WriteLine(LastName.Key + " - " + LastName.Count());

                foreach (Person individual in LastName) {
                   System.Console.WriteLine("\t{0} - {1}", individual.LastName, individual.FirstName);
                }
            }
        }
    }
    
    public class Person{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
