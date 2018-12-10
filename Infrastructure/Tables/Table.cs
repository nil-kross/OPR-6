using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.Tables
{
    public class CellsSpan
    {
        private Cell[] cellsArray;
        
        public CellsSpan(IEnumerable<Cell> cells)
        {
            this.cellsArray = new Cell[cells.Count()];

            {
                var c = 0;

                foreach (var cell in cells)
                {
                    this.cellsArray[c] = cell;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("Amount: {0}", this.cellsArray.Length);
        }
    }

    public class Table
    {
        private Cell[,] cellsArray = null;
        private Int32 rowsValue = 0;
        private Int32 colsValue = 0;

        public IList<CellsSpan> Rows {
            get {
                IList<CellsSpan> rowsList = new List<CellsSpan>();

                for (var r = 0; r < this.RowsAmount; r++)
                {
                    rowsList.Add(this.GetRow(r));
                }

                return rowsList;
            }
        }
        public IList<CellsSpan> Cols {
            get {
                IList<CellsSpan> colsList = new List<CellsSpan>();

                for (var r = 0; r < this.ColsAmount; r++)
                {
                    colsList.Add(this.GetCol(r));
                }

                return colsList;
            }
        }
        public Int32 RowsAmount {
            get {
                return this.rowsValue;
            }
            set {
                this.rowsValue = value;
            }
        }
        public Int32 ColsAmount {
            get {
                return this.colsValue;
            }
            set {
                this.colsValue = value;
            }
        }
        public Cell this[int row, int col] {
            get {
                if (row >= 0 && row < this.RowsAmount && col >= 0 && col < this.ColsAmount)
                {
                    return this.cellsArray[row, col];
                } else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            set {
                if (row >= 0 && row < this.RowsAmount && col >= 0 && col < this.ColsAmount) {
                    this.cellsArray[row, col] = value;
                } else {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Table(int rows, int cols)
        {
            this.RowsAmount = rows;
            this.ColsAmount = cols;
            this.cellsArray = new Cell[rows, cols];
        }

        public Table ForEach(Func<Cell, Int32, Int32, Cell> func)
        {
            for (var r = 0; r < this.RowsAmount; r++)
            {
                for (var c = 0; c < this.ColsAmount; c++)
                {
                    this[r, c] = func(this[r, c], r, c);
                }
            }

            return this;
        }

        public Table ForEach(Func<Cell, Cell> func)
        {
            return this.ForEach((old, r, c) => func(old));
        }

        public override string ToString()
        {
            return String.Format("{0} x {1}", this.RowsAmount, this.ColsAmount);
        }

        protected CellsSpan GetRow(Int32 row)
        {
            CellsSpan cellsSpan = null;

            {
                Cell[] cellsArray = new Cell[this.colsValue];

                for (var c = 0; c < this.colsValue; c++)
                {
                    cellsArray[c] = this.cellsArray[row, c];
                }

                cellsSpan = new CellsSpan(cellsArray);
            }

            return cellsSpan;
        }

        protected CellsSpan GetCol(Int32 col)
        {
            CellsSpan cellsSpan = null;

            {
                Cell[] cellsArray = new Cell[this.rowsValue];

                for (var c = 0; c < this.colsValue; c++)
                {
                    cellsArray[c] = this.cellsArray[c, col];
                }

                cellsSpan = new CellsSpan(cellsArray);
            }

            return cellsSpan;
        }
    }
}