using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GOLConsoleApp
{
    public class Generation
    {
        internal GameOfLife Game { get; set; }
        public int GenerationNumber { get; set; }
        public List<List<Cell>> Rows { get; set; }


        public Generation(GameOfLife gameOfLife)
        {
            this.Game = gameOfLife;
            this.InitGeneration();
        }

        public Generation(Generation generation)
        {
            this.Game = generation.Game;
            this.AdvanceGeneration(generation);

        }
        private void InitGeneration()
        {
            this.GenerationNumber = 1;
            this.Rows = new List<List<Cell>>();
            for (var rowIndex = 0; rowIndex < this.Game.Height; rowIndex++)
            {
                var row = new List<Cell>();
                this.Rows.Add(row);
                for (var colIndex = 0; colIndex < this.Game.Width; colIndex++)
                {
                    row.Add(new Cell(rowIndex, colIndex));
                }
            }
        }

        internal void Print()
        {
            var sb = new StringBuilder();
            this.Rows.ForEach(row => this.AddRow(sb, row));
            Console.SetCursorPosition(0, 0);
            Console.Write(sb.ToString());
        }

        private void AddRow(StringBuilder sb, List<Cell> row)
        {
            var rowStr = String.Join(String.Empty, row.Select(cell => cell.IsAlive ? "X" : " "));
            sb.AppendLine(rowStr);
        }

        private void AdvanceGeneration(Generation parentGeneration)
        {
            this.GenerationNumber = parentGeneration.GenerationNumber + 1;
            Console.WriteLine("Advancing Generation {0}", this.GenerationNumber);
            this.Rows = JsonConvert.DeserializeObject<List<List<Cell>>>(JsonConvert.SerializeObject(parentGeneration.Rows));
            this.Rows.ForEach(row => row.ForEach(cell => cell.ParentGeneration = parentGeneration));
            this.ProcessLife();
            //Thread.Sleep(1000);
        }

        private void ProcessLife()
        {
            this.Rows.ForEach(row => row.ForEach(cell => cell.ProcessLife()));
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}