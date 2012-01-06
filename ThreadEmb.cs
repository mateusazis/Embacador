using System;
using System.Drawing;
namespace Embacador
{
    class ThreadEmb
    {
        Bitmap bitmap;
        int coef, x0, qtdX, y0, qtdY;
        int threadIDi, threadIDj;

        public ThreadEmb(Bitmap bitmap, int coef, int minX, int qtdX, int minY, int qtdY, int threadIDi, int threadIDj)
        {
            this.bitmap = bitmap;
            this.coef = coef;
            this.x0 = minX;
            this.qtdX = qtdX;
            this.y0 = minY;
            this.qtdY = qtdY;
            this.threadIDi = threadIDi;
            this.threadIDj = threadIDj;
        }

        public void Iniciar()
        {
            Console.WriteLine("Iniciando thread {0},{1}.", threadIDi, threadIDj);
            Graphics graphics = Graphics.FromImage(bitmap);
            int i, j;
            int k, l;
            int r, g, b;
            int framesUsados;
            Color c, nova;
            Brush brush;
            int xMax = x0 + qtdX;
            int yMax = y0 + qtdY;
            for (i = y0; i < yMax; i++)
                for (j = x0; j < xMax; j++)
                {
                    r = 0; g = 0; b = 0; framesUsados = 0;
                    for (k = -coef; k <= coef; k++)
                        for (l = -coef; l <= coef; l++)
                        {
                            try
                            {
                                c = bitmap.GetPixel(j + l, i + k);
                                r += c.R;
                                g += c.G;
                                b += c.B;
                                framesUsados++;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                            }

                        }
                    r /= framesUsados;
                    g /= framesUsados;
                    b /= framesUsados;
                    nova = Color.FromArgb(r, g, b);
                    brush = new SolidBrush(nova);
                    graphics.FillRectangle(brush, j, i, 1, 1);
                }
            EmbacadorMultiThread.threadCompletas++;
            Console.WriteLine("Terminando thread {0},{1}.", threadIDi, threadIDj);
            if (EmbacadorMultiThread.threadCompletas == EmbacadorMultiThread.totalThreads)
                EmbacadorMultiThread.threadPrincipal.Resume();
        }

        public void desenhaResultado(Graphics g)
        {
            Rectangle src = new Rectangle(x0, y0, qtdX, qtdY);
            g.DrawImage(bitmap, src, src, GraphicsUnit.Pixel);
        }
    }
}
