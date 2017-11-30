using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Threading.AutoResetEvent;

namespace HorseThreads
{
    class Program
    {
        static void Main(string[] args)
        {

            //  Horse[] horses = new Horse[6];


            ConcurrentQueue<Tuple<Thread,AutoResetEvent >> threads = new ConcurrentQueue<Tuple<Thread, AutoResetEvent>>();
            ManualResetEvent c = new ManualResetEvent(initialState: false);
            for (int i = 1; i < 16; i++)
            {
                AutoResetEvent w=   new AutoResetEvent(initialState: false);
                Horse horse = new Horse() { Row = i, Start = c, Name=$"horse {i}", Winner=w };
                threads.Enqueue(item: new Tuple<Thread, AutoResetEvent>(item1: new Thread(start: horse.Run ){Name = i.ToString()},item2: w));

            }

            foreach (var th in threads)
            {
                th.Item1.Start();
            }

            c.Set();


            WaitHandle[] autoResetEvents = threads.Select(selector: ct=>ct.Item2).ToArray<AutoResetEvent>();
            int winner = WaitHandle.WaitAny(waitHandles: autoResetEvents);

            Console.ForegroundColor = ConsoleColor.DarkRed;
              Console.WriteLine(value: $"### winner is {threads.Select(selector: ct => ct.Item1).ToArray<Thread>()[winner].Name  }");

            Console.WriteLine(value: "");
            Console.ReadLine();
        }

       
    }

   
}
