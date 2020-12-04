using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejercicio6
{
    class Program
    {
        static bool flag = true;
        static bool start = true;
        static string winner ="";
        static string loser ="";
        static int a, b;
        static int i = 0;
        static int result;
        static readonly object l = new object();
        static Random generador = new Random();
        static char[] simbolos = new char[] { '/', '\\', '|', '-' };
        static void NumAleatorio()
        {

            while (start)
            {
                lock (l)
                {
                    if (start)
                    {
                       
                        a = generador.Next(1, 11);

                        if (a == 5 || a == 7)
                        {
                            if (!flag)
                            {
                                result += 10;
                            }
                            else
                            {
                            result ++;
                            flag = false;

                            }

                        }
                        if (result >= 20)
                        {
                            winner = "Player 1";
                            result = 20;
                            start = false;
                            Monitor.Pulse(l);
                        }
                        else
                        {
                            loser = "Player 2";
                        }
                    }
                    Console.SetCursorPosition(1, 1);
                    Console.WriteLine("Player 1: " + a);
                }
                Thread.Sleep(generador.Next(100, 100 * a));
            }
        }
        static void NumAleatorio2()
        {

            while (start)
            {
                lock (l)
                {
                    if (start)
                    {
                        b = generador.Next(1, 11);
                        if (b==5 || b==7)
                        {
                            if (flag && result!=0)
                            {
                                result -= 10;
                            }
                            else
                            {
                                result--;
                                flag = true;
                                Monitor.Pulse(l);
                            }
                        }
                        if (result <= -20)
                        {
                            winner = "Player 2";
                            start = false;
                        }
                        else
                        {
                            loser = "Player 1";
                        }
                        Console.SetCursorPosition(1, 3);
                        Console.WriteLine("Player 2: " + b);
                    }
                }
                Thread.Sleep(generador.Next(100, 100 * b));
            }
        }
        static public void Display()
        {
            
            while (start)
            {
                lock (l)
                {
                    if (start)
                    {
                        if (flag)
                        {
                            Console.SetCursorPosition(10, 5);
                            Console.WriteLine(simbolos[i]);
                            i++;
                            if (i==simbolos.Length)
                            {
                                i = 0;
                            }
                        }
                        else
                        {
                            Monitor.Wait(l);
                        }
                    }
                }
                Thread.Sleep(200);
            }

        }
        static void Main(string[] args)
        {
            Thread player1 = new Thread(NumAleatorio);
            Thread player2 = new Thread(NumAleatorio2);
            Thread display = new Thread(Display);
            display.IsBackground = true;

            player1.Start();
            player2.Start();
            display.Start();

            player1.Join();
            player2.Join();

            Console.SetCursorPosition(0, 5);

            Console.WriteLine("Winner: {0,5} Loser: {1,5}",winner,loser);

            Console.ReadLine();

        }

    }
}
