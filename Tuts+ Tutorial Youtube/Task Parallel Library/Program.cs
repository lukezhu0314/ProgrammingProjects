using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Parallel_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new CancellationTokenSource();
            try{
                var t1 = Task.Factory.StartNew(() => DoSomeVeryImportantWork(1, 1200, source.Token)).ContinueWith((prevTask) => DoSomeOtherVeryImportantWork(1, 3000, source.Token));
                source.Cancel();
            
            }catch(Exception ex){
                Console.WriteLine(ex.GetType());
            }finally{
                
            }
            
            /*
            var intList = new List<int>{1, 2, 5, 6, 7, 978, 243, 54, 656, 67, 34, 5, 4, 9};

            Parallel.ForEach(intList, (i) => Console.WriteLine(i));
            Parallel.For(0, 100, (i) => Console.WriteLine(i));
            */

            /*
            var t1 = Task.Factory.StartNew(() => DoSomeVeryImportantWork(1,1500));
            
            var t2 = Task.Factory.StartNew(() => DoSomeVeryImportantWork(2,3000));
            
            var t3 = Task.Factory.StartNew(() => DoSomeVeryImportantWork(3,1000));
            
            //What's the major difference between a Task and an array?
            var taskList = new List<Task> {t1, t2, t3};
            Task.WaitAll(taskList.ToArray());
            //This way seems to work as well: Task.WaitAll(t1, t2, t3); => go check the overload for Task.WaitAll
            */

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
            
        }

        static void DoSomeVeryImportantWork (int id, int sleepTime, CancellationToken token) {
            if(token.IsCancellationRequested) {
                Console.WriteLine("Cancellation requested.");
                token.ThrowIfCancellationRequested();
            }
            Console.WriteLine("task {0} is beginning", id);
            Thread.Sleep(sleepTime);
            Console.WriteLine("task {0} has completed", id);
        }

        static void DoSomeOtherVeryImportantWork (int id, int sleepTime, CancellationToken token) {
            if(token.IsCancellationRequested) {
                Console.WriteLine("Cancellation requested.");
                token.ThrowIfCancellationRequested();
            }
            Console.WriteLine("task {0} is beginning more work",id);
            Thread.Sleep(sleepTime);
            Console.WriteLine("Task {0} has completed more work", id);
        }
    }
}
