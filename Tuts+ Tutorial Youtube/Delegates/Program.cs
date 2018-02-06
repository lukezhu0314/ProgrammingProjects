using System;

namespace Delegates
{
    //When declaring a delegate class, need to specify the return type (has to match the return type of the encapsulating method), specify the name of the delegate, as well as the accepting parameters (has to match the parameter types of the method as well)
    delegate void MyDelegate(string name);
    //When the compiler processes the delegate line of code, it actually constructs a class => class MyDelegate(), so the operations on a delegate is no different from the operations on a class
    delegate void Operation(int number);
    class Program
    {
        static void Main(string[] args)
        {
            /*
            
            //We are only passing the method into the delegate, but not executing it, therefore we don't need the () trailing the method name
            //One way of encapsulating the method:
            
            MyDelegate del1 = new MyDelegate(SayHello);
            
            //We can also choose to omit the new MyDelegate declaration
            
            MyDelegate del2 = SayHello;
            
            //Or we can write a method that returns a delegate Mydelegate, and assign it to a variable in the main body
            
            MyDelegate del3 = GiveMeMyDelegate();
            
            //Invoke the delegate method, the longer way to do it is del.Invoke();
            
            Test(del3);
            
             */
            
            Operation op = Double;
            ExecuteOperation(2, op);

            op = Triple;
            ExecuteOperation(2, Triple);

            //Or we can do delegate chaining
            Operation opChaining = Double;
            opChaining += Triple;
            ExecuteOperation(2, opChaining);
        }

        static void Double (int num){
            System.Console.WriteLine("{0} x 2 = {1}", num, num * 2);
        }

        static void Triple (int num){
            System.Console.WriteLine("{0} x 3 = {1}", num, num * 3);
        }

        static void ExecuteOperation(int num, Operation operation) {
            operation(num);
        }
        static void SayHello(string name)
        {
            System.Console.WriteLine("Hey There, {0}", name);
        }

        //Method that takes in a delegate as parameter and invokes the encapsulating function
        static void Test(MyDelegate del){
            del("John");
        }

        //Method that returns the MyDelegate type
        static MyDelegate GiveMeMyDelegate(){
            MyDelegate del = SayHello;
            return del;
        }
    }
}
