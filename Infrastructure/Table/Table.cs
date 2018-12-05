using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.Table
{
    public class Table
    {
        private Cell[,] cellsArray = null;

        public Int32 Rows { get; private set; }
        public Int32 Cols { get; private set; }
        public Cell this[int row, int col] {
            get {
                if (row >= 0 && row < this.Rows && col >= 0 && col < this.Cols)
                {
                    return this.cellsArray[row, col];
                } else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Table(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
        }
    }
}