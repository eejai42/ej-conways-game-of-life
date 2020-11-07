using System;
using System.Runtime.CompilerServices;

namespace GOLConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var gol = new GameOfLife(44, 44);
            gol.WrapCells = false;
            gol.LoadPNG(@"../../../glider_gun.png", 38, 11);
            //gol.LoadPNG(@"../../../wiggler.png", 44, 44);
            gol.RunForThisManyGenerations(1000);
            Console.WriteLine("Game of life complete");
            Console.ReadKey();
        }
    }
}
