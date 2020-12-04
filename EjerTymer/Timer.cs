using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjerTymer
{
    public delegate void MyTimer();
    class Timer
    {
        static readonly private object l = new object();
        public int intervalo;
        static bool flag= true;
        static int cont=0;


        Thread thread = new Thread(run);
        public Timer(MyTimer t){

            while (flag)
            {

                thread.Start();
            }

            
        }

        static void run()
        {

            while (flag)
            {
                if (flag)
                {
                    lock (l)
                    {
                        cont++;
                        Console.WriteLine(cont);

                    }

                }

            }

        

        }
        


    }
}
