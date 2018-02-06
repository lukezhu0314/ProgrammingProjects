using System;

namespace Anonymous_Methods_and_Lambda_Expressions
{
    class Program
    {
        delegate void Operation(int num);
        static void Main(string[] args)
        {
            Operation op = Double;
            op(2);

            //The inline anonymous way, think of this as similar to how JavaScript declares anonymous functions, except for the key word 'function' is replaced by 'delegate'
            /*
            var a = function(num) { .... }
             */
            Operation opInline = delegate(int num){
                Console.WriteLine("{0} x 2 = {1}", num, num * 2);
            };
            opInline(2);

            //The lamdba expression way
            Operation opLambda = (int num) => {Console.WriteLine("{0} x 2 = {1}", num, num * 2);};
            opLambda(2);

            //Or we can use generic delegates: Action<> or Func<>
            Action<int> opAction= num => {Console.WriteLine("{0} x 2 = {1}", num, num * 2);};
            opAction(2);

            Func<int,int> opFunc = num => {return num * 2;};
            Console.WriteLine(opFunc(2));
        }

        static void Double(int num){
            System.Console.WriteLine("{0} x 2 = {1}", num, num * 2);
        }
    }
}
