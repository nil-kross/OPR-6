using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.GamesTheory {
    public class Matrix {
        private Double[][] array;
        private Byte rowsCountValue;
        private Byte colsCountValue;

        public Byte RowsCount {
            get { return this.rowsCountValue; }
        }
        public Byte ColsCount {
            get {
                return this.colsCountValue;
            }
        }
        public Double this[Byte row, Byte col] {
            get {
                if (!(row >= 0 && row < this.RowsCount && col >= 0 && col < this.ColsCount)) {
                    throw new IndexOutOfRangeException();
                }

                return this.array[row][col];
            }
        }

        public Matrix(Byte rows, Byte cols) {
            this.rowsCountValue = rows;
            this.colsCountValue = cols;
        }

        public override String ToString() {
            return String.Format("M {0}x{1}", this.RowsCount, this.ColsCount);
        }
    }
}
