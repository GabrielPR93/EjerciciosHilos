using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjerCaballos
{
    class Program       //REVISAR
    {
        static readonly object l = new object();
        static bool flag = true;
        static int campeon;
        static void carrera(object obj)
        {
            Caballo caba = (Caballo)obj;
            Random generador = new Random();

            while (flag)
            {
                if (flag)
                {
                    lock (l)
                    {
                        Console.SetCursorPosition(caba.PosX, caba.PosY);
                        Console.WriteLine(" ");
                        caba.PosX += generador.Next(2, 10);
                        Console.SetCursorPosition(caba.PosX, caba.PosY);

                        if (caba.PosX < 100)
                        {
                            Console.WriteLine(caba.NumCaballo);
                        }
                        else
                        {
                            campeon = caba.PosX;
                            Console.WriteLine("Campeon " + campeon);
                            flag = false;
                            Monitor.Pulse(l);
                        }
                    }
                }
            }
            Thread.Sleep(generador.Next(100, 400));
        }
        static int pedirCaballo()
        {
            int opcion = 0;
            bool flag;
            do
            {
                flag = true;
                try
                {

                    Console.WriteLine("Introduce tu caballo ganador");
                    Console.WriteLine("1/2/3/4/5");
                  
                    opcion = Convert.ToInt32(Console.ReadLine());

                }
                catch (FormatException)
                {
                    Console.WriteLine("Error al seleccionar caballo");
                    flag = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Error al seleccionar caballo, numero fuera de rango");
                    flag = false;
                }
            } while (!flag);


            return opcion;
        }

    
        static void Main(string[] args)
        {
            Thread[] hilos = new Thread[5];
            Caballo[] caballos = new Caballo[5];

            int volverJugar;
            bool flag2;
            int opcionJugador;

            do
            {
                flag = false;
               opcionJugador = pedirCaballo();
                

                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i] = new Caballo(i, i);
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(caballos[i].NumCaballo);
                    hilos[i] = new Thread(carrera);
                }

                for (int i = 0; i < caballos.Length; i++)
                {
                    hilos[i].Start(caballos[i]);
                    flag = true;
                }

                lock (l)
                {
                    while (flag)
                    {
                        Monitor.Wait(l);
                    }
                }
                Console.SetCursorPosition(0,10);
                Console.WriteLine("CAMPEON " + campeon);

                if (opcionJugador == campeon)
                {
                    Console.Clear();
                    Console.WriteLine("Enhorabuena ganaste !!!");
                }
                else
                {
                 
                    Console.WriteLine("Perdiste !!!");
                    do
                    {
                        flag2 = true;
                        try
                        {
                            Console.WriteLine("Volver a jugar ?? (1-Si/2-No)");
                            volverJugar = Convert.ToInt32(Console.ReadLine());

                            if (volverJugar == 1)
                            {
                                flag = true;
                                Console.Clear();
                            }
                        }
                        catch (Exception)
                        {
                            flag2 = false;
                            Console.WriteLine("Error selecciona una opcion");
                        }
                    } while (!flag2);
                }

            } while (flag);

        }
    }
}
