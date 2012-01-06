using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Embacador
{
    class Inicio
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Embacador de imagens por Mateus Azis");
            Console.Write("Deseja usar o modo multithread (S/N)? ");
            string resp = Console.ReadLine();
            if (resp.Equals("S"))
                EmbacadorMultiThread.Main(args);
            else
                Program.Main(args);
        }
    }
}
