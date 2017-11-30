using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace HorseThreads
{
  public   class Horse
    {
    
        public string Name { get; set; }

        public ManualResetEvent Start { get; set; }

      
        public AutoResetEvent Winner { get; set; }

        private readonly Object _locker = new object();

        public  int Row { get; set; }
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        public void Run()
        {
            Start.WaitOne();
          
            
                for (int i = 0; i < 30; i++)
                {
                    var millisecondsTimeout = _random.Next(50, 500);
                    Thread.Sleep(millisecondsTimeout);
                    lock (_locker)
                    {
                        Console.ForegroundColor = (ConsoleColor)Row;
                        Console.SetCursorPosition(i, Row);
                        Console.Write("*");
                    }
            }
            Winner.Set();
        }


    }
}
