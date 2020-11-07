using GOLConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameOfLifeTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var gol = new GameOfLife(50, 50);
            gol.LoadPNG(@"../../../glider_gun.png", 38, 11);
            gol.RunForThisManyGenerations(1000);
            Console.WriteLine("Game of life complete");
        }
    }
}
