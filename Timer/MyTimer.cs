using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Timer
{
    public delegate void Delegado();
    class MyTimer
    {
        public int intervalo;
        public bool flag = true;
        public bool runing = true;
        Delegado del;


        static readonly object l = new object();

        public MyTimer(Delegado del)
        {
            Thread thread = new Thread(hilo);
            this.del = del;
            thread.IsBackground = true;
            thread.Start();

        }

        public void hilo()
        {

            while (runing)
            {
                lock (l)
                {
                    if (flag)
                    {
                        Monitor.Wait(l);
                    }
                    del();

                    Thread.Sleep(intervalo);
                }
            }
        }

        public void pause()
        {
            lock (l)
            {
                flag = true;
            }

        }

        public void run()
        {
            lock (l)
            {
                Monitor.Pulse(l);
                flag = false;


            }
        }










    }
}
