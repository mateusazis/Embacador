using System;
using System.Drawing;
using System.Threading;

namespace Embacador
{
    class EmbacadorMultiThread
    {
        public static int threadCompletas = 0;
        public static int totalThreads;
        public static Thread threadPrincipal;
        public static void Main(string[] args)
        {
            Console.Write("Diga o nome do arquivo de img: ");
            string nomeArq = Console.ReadLine();
            Console.Write("Diga o tamanho do embacamento: ");
            int coef = Int32.Parse(Console.ReadLine());
            Image imagem = Image.FromFile(nomeArq);
            Bitmap bitmap = new Bitmap(imagem);
            Console.Write("Quantas thread horizontais? ");
            int threadsH = Int32.Parse(Console.ReadLine());
            Console.Write("Quantas thread verticais? ");
            int threadsV = Int32.Parse(Console.ReadLine());
            totalThreads = threadsH * threadsV;

            ThreadEmb[,] threads = new ThreadEmb[threadsV, threadsH];

            int i, j;
            int qtdX = bitmap.Width / threadsH;
            int qtdY = bitmap.Height / threadsV;
            Graphics g = Graphics.FromImage(bitmap);
            
            for(i = 0; i < threadsV; i++)
                for (j = 0; j < threadsH; j++)
                    threads[i, j] = new ThreadEmb((Bitmap)bitmap.Clone(), coef, qtdX * j, qtdX, qtdY * i, qtdY, i, j);

            Thread t;
            ThreadStart ts;

            threadPrincipal = Thread.CurrentThread;

            for (i = 0; i < threadsV; i++)
                for (j = 0; j < threadsH; j++)
                {
                    ts = new ThreadStart(threads[i, j].Iniciar);
                    t = new Thread(ts);
                    t.Start();
                }

            //while (threadCompletas < totalThreads) { }

            threadPrincipal.Suspend();

            for (i = 0; i < threadsV; i++)
                for (j = 0; j < threadsH; j++)
                    threads[i,j].desenhaResultado(g);

            bitmap.Save("editado_" + nomeArq);
        }
    }
}

