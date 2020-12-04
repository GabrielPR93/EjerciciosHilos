using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerCaballos
{
    class Caballo
    {
        public int PosX { set; get; }
        public int PosY { set; get; }
        public int NumCaballo { set; get; }

        public Caballo(int PosY,int NumCaballo)
        {
            this.PosX = 0;
            this.PosY = PosY;
            this.NumCaballo = NumCaballo;

        }

        public int corre()
        {
            Random generador = new Random();
            return generador.Next(1,6);
        }
        

    }
}
