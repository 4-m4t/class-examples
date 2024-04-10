using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace class_examples
{
    public class Timer
    {
        public List<System.Threading.Thread> list { get; set; }

        public void Create(string uniqueID, int RepeatCount, int Tick, Action function, Action EndFunction)
        {
            Thread timer_thread = new Thread(() => timer_func(RepeatCount, Tick, function, EndFunction));
            timer_thread.Name = uniqueID;
            timer_thread.Start();


            if (list != null)
                list.Add(timer_thread);
            else
            {
                list = new List<System.Threading.Thread>
                        {
                            timer_thread
                        };
            }

        }

        public void Stop(string uniqueID)
        {
            foreach (var thread in list)
            {
                if (thread.Name == uniqueID)
                {
                    thread.Abort();
                }
            }
        }

        void timer_func(int RepeatCount, int Tick, Action function, Action EndFunction)
        {
            if (RepeatCount == 0)
            {
                RepeatCount = 1000;
            }

            Tick = Tick * 1000;

            for (int i = 0; i < RepeatCount; i++)
            {
                Thread.Sleep(Tick);
                function();
            }
            EndFunction();
        }

    }
}
