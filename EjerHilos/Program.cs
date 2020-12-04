using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjerHilos
{
    class Program
    {

        static readonly private object l = new object();
        static int valor = 0;
        static bool flag;
        static void Main(string[] args)
        {
            flag = true;
        //    Thread thread = new Thread(decrementa);
        //    Thread thread2 = new Thread(incrementa);
        //    thread.Start();
        //    thread2.Start();

        //    thread.Join();

        //    if (valor > 0)
        //    {
        //        Console.WriteLine("Ganó el hilo 1");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ganó el hilo 2");
        //    }


        //}
        //static void incrementa()
        //{

        //    while (flag)
        //    {
        //        lock (l)
        //        {
        //            if (flag)
        //            {

        //                valor++;
        //                if (valor == 100)
        //                {
        //                    flag = false;
        //                }
        //                Console.WriteLine("Thread 1 --> " + valor);
        //            }

        //        }
        //    }
        //    Console.ReadLine();

        //}
        //static void decrementa()
        //{

        //    while (flag)
        //    {
        //        lock (l)
        //        {
        //            if (flag)
        //            {
        //                valor--;

        //                if (valor == -100)
        //                {
        //                    flag = false;
        //                }
        //                Console.WriteLine("thread 2 --> " + valor);
        //            }
        //        }

        //    }
        //}

        Thread thread1 = new Thread(() =>
          {
              while (flag)
              {
                  lock (l)
                  {
                      if (flag)
                      {
                          valor++;
                          if (valor >= 100)
                          {
                              flag = false;
                          }
                          Console.WriteLine("Thread 1 --- " + valor);
                      }

                  }
              }
          });
        Thread thread2 = new Thread(() =>
                   {
                       while (flag)
                       {
                           lock (l)
                           {
                               if (flag)
                               {
                                   valor--;
                                   if (valor <= -100)
                                   {
                                       flag = false;
                                   }
                                   Console.WriteLine("Thread 2 --- " + valor);
                               }

                           }
                       }
                   });
        thread1.Start();
        thread2.Start();


        thread1.Join();
        if (valor>0)
        {
            Console.WriteLine("Ganó el hilo 1");
        }
        else
        {
            Console.WriteLine("Ganó el hilo 2");
        }
                           Console.ReadLine();
    }

    }

}

