using System;
using System.Runtime.CompilerServices;

namespace GOLConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var gol = new GameOfLife(50, 50);
            gol.LoadPNG(@"../../../glider_gun.png", 38, 11);
            gol.RunForThisManyGenerations(1000);
            Console.WriteLine("Game of life complete");
            Console.ReadKey();
        }
    }
}
