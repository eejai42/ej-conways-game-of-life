using System;

namespace GOLConsoleApp
{
    public class Cell
    {
        public static Random Rand = new Random();

        internal Generation ParentGeneration { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public bool IsAlive { get; set; }
        public Cell(int rowIndex, int colIndex)
        {
            this.ParentGeneration = null;
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
            //this.IsAlive = Rand.Next(100) >= 50;
            this.IsAlive = false;
        }

        internal void ProcessLife()
        {
            var neighbors = this.GetParentLivingNeighbors();
            if (this.ParentIsAlive())
            {
                if (neighbors == 2 || neighbors == 3) this.IsAlive = true;
                else this.IsAlive = false;
            }
            else
            {
                if (this.GetParentLivingNeighbors() == 3) this.IsAlive = true;

            }
        }

        private int GetParentLivingNeighbors()
        {
            var neighbors = 0;
            neighbors += this.GetIsParentAlive(RowIndex - 1, ColIndex - 1);
            neighbors += this.GetIsParentAlive(RowIndex - 1, ColIndex);
            neighbors += this.GetIsParentAlive(RowIndex - 1, ColIndex + 1);
            neighbors += this.GetIsParentAlive(RowIndex, ColIndex - 1);
            neighbors += this.GetIsParentAlive(RowIndex, ColIndex + 1);
            neighbors += this.GetIsParentAlive(RowIndex + 1, ColIndex - 1);
            neighbors += this.GetIsParentAlive(RowIndex + 1, ColIndex);
            neighbors += this.GetIsParentAlive(RowIndex + 1, ColIndex + 1);
            return neighbors;
        }

        private int GetIsParentAlive(int row, int col)
        {
            var wrap = this.ParentGeneration.Game.WrapCells;
            var width = this.ParentGeneration.Game.Width;
            var height = this.ParentGeneration.Game.Height;
            if (row < 0)
            {
                if (wrap) row = height - 1;
                else return 0;
            }
            else if (row >= height)
            {
                if (wrap) row = 0;
                else return 0;

            }
            if (col < 0)
            {
                if (wrap) col = width - 1;
                else return 0;
            }
            else if (col >= width)
            {
                if (wrap) col = 0;
                else return 0;
            }
            return this.ParentGeneration.Rows[row][col].IsAlive ? 1 : 0;
        }

        internal void Print()
        {
            Console.SetCursorPosition(this.ColIndex, this.RowIndex);
            if (this.IsAlive) Console.Write("X");
            else Console.Write(" ");
        }

        private bool ParentIsAlive()
        {
            if (this.ParentGeneration is null) return false;
            return this.ParentGeneration.Rows[this.RowIndex][this.ColIndex].IsAlive;
        }

        public override string ToString()
        {
            return String.Format("{0}x{1} {2}", this.ColIndex, this.RowIndex, this.IsAlive ? "X" : "");
        }
    }
}