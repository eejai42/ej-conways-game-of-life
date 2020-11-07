using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace GOLConsoleApp
{
    public class GameOfLife
    {
        public int Width { get; set; }
        public int Height { get; set; }

        internal void LoadPNG(string path, int xSegments, int ySegments)
        {
            using (var bitmap = (Bitmap)Image.FromFile(path))
            {
                var xLength = bitmap.Width / xSegments;
                var yLength = bitmap.Height / ySegments;

                for (var y = 0; y < ySegments; y++)
                {
                    for (var x = 0; x < xSegments; x++)
                    {
                        var xPos = (xLength * x) + (xLength / 2);
                        var yPos = ((yLength * y) + (yLength / 2));
                        var color = bitmap.GetPixel(xPos, yPos);
                        //Console.WriteLine("CHECKING: {0:0000}x{1:0000} - {2}", xPos, yPos, color);
                        if (color.R + color.G + color.B < 100)
                        {
                            //Console.WriteLine("FOUND: {0}x{1} - {2}", x, y, color);
                            this.CurrentGeneration.Rows[y][x].IsAlive = true;
                        }
                        else this.CurrentGeneration.Rows[y][x].IsAlive = false;
                    }
                }
            }

        }

        internal Generation CurrentGeneration { get; set; }

        public GameOfLife(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.CurrentGeneration = new Generation(this);
        }

        public void RunForThisManyGenerations(int generations)
        {
            this.CurrentGeneration.Print();
            for (int i = 0; i < generations; i++)
            {
                this.CurrentGeneration = new Generation(this.CurrentGeneration);
                this.CurrentGeneration.Print();
            }
        }
    }
}