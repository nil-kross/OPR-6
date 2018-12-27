using System;
using System.Collections.Generic;
using System.Linq;

namespace Lomtseu.GamesTheory
{
    public class Span
    {
        private Double[] array;

        public Span(IEnumerable<Double> items)
        {
            this.array = new Double[items.Count()];

            {
                var c = 0;

                foreach (Double item in items)
                {
                    this.array[c] = item;
                }
            }
        }

        public override String ToString()
        {
            return String.Format("Amount: {0}", this.array.Length);
        }
    }

    public class Matrix {
        private Double[][] array;
        private Byte rowsValue;
        private Byte colsValue;

        public Byte RowsCount {
            get { return this.rowsValue; }
        }
        public Byte ColsCount {
            get {
                return this.colsValue;
            }
        }
        public IList<Span> Rows {
            get {
                IList<Span> rowsList = new List<Span>();

                for (var r = 0; r < this.RowsCount; r++)
                {
                    rowsList.Add(this.GetRow(r));
                }

                return rowsList;
            }
        }
        public IList<Span> Cols {
            get {
                IList<Span> colsList = new List<Span>();

                for (var r = 0; r < this.ColsCount; r++)
                {
                    colsList.Add(this.GetCol(r));
                }

                return colsList;
            }
        }
        public Double this[Byte row, Byte col] {
            get {
                if (!(row >= 0 && row < this.RowsCount && col >= 0 && col < this.ColsCount)) {
                    throw new IndexOutOfRangeException();
                }

                return this.array[row][col];
            }
            set {
                if (!(row >= 0 && row < this.RowsCount && col >= 0 && col < this.ColsCount))
                {
                    throw new IndexOutOfRangeException();
                }

                this.array[row][col] = value;
            }
        }

        public Matrix(Byte rows, Byte cols) {
            this.rowsValue = rows;
            this.colsValue = cols; 
            this.ForEach((c) => 0.0);
        }
        public Matrix(Double[][] array)
        {
            this.rowsValue = (Byte)array.Length;
            this.colsValue = (Byte)array[0].Length;

            for (Byte r = 0; r < this.RowsCount; r++)
            {
                for (Byte c = 0; c < this.ColsCount; c++)
                {
                    this[r, c] = array[r][c];
                }
            }
        }

        public Matrix ForEach(Func<Double, Byte, Byte, Double> func)
        {
            for (Byte r = 0; r < this.RowsCount; r++)
            {
                for (Byte c = 0; c < this.ColsCount; c++)
                {
                    this[r, c] = func(this[r, c], r, c);
                }
            }

            return this;
        }

        public Matrix ForEach(Func<Double, Double> func)
        {
            return this.ForEach((old, r, c) => func(old));
        }

        public Matrix Rotate()
        {
            Matrix matrix = new Matrix(this.ColsCount, this.RowsCount);

            matrix.ForEach((item, row, col) => this[col, row]);

            return matrix;
        }

        public Double[][] ToArray()
        {
            return this.array;
        }

        public override String ToString() {
            return String.Format("M {0}x{1}", this.RowsCount, this.ColsCount);
        }

        protected Span GetRow(Int32 row)
        {
            Span cellsSpan = null;

            {
                Double[] cellsArray = new Double[this.colsValue];

                for (var c = 0; c < this.colsValue; c++)
                {
                    cellsArray[c] = this.array[row][c];
                }

                cellsSpan = new Span(cellsArray);
            }

            return cellsSpan;
        }

        protected Span GetCol(Int32 col)
        {
            Span cellsSpan = null;

            {
                Double[] cellsArray = new Double[this.rowsValue];

                for (var c = 0; c < this.colsValue; c++)
                {
                    cellsArray[c] = this.array[c][col];
                }

                cellsSpan = new Span(cellsArray);
            }

            return cellsSpan;
        }
    }
}
