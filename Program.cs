using System;
using System.Drawing;
namespace Embacador
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Diga o nome do arquivo de img: ");
            string nomeArq = Console.ReadLine();
            Console.Write("Diga o tamanho do embacamento: ");
            int coef = Int32.Parse(Console.ReadLine());
            Image imagem = Image.FromFile(nomeArq);
            Bitmap bitmap = new Bitmap(imagem);
            Graphics graphics = Graphics.FromImage(bitmap);
            int i, j;
            int k, l;
            int r, g, b;
            int framesUsados;
            Color c, nova;
            Brush brush;
            for(i = 0; i < bitmap.Height; i++)
                for (j = 0; j < bitmap.Width; j++)
                {
                    r = 0; g = 0; b = 0; framesUsados = 0;
                    for(k = -coef; k <= coef; k++)
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
            bitmap.Save("editado_" + nomeArq);
        }
    }
}
