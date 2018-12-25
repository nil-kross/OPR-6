using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.GamesTheory {
    public class StrategiesTable {
        private Double[][] array;
        private Direction direction;

        public Double[][] Strategies {
            get { return this.array; }
        }
        public Direction Direction {
            get { return this.direction; }
        }
        public Int32 Length {
            get { return this.array[0].Length; }
        }
        public Double[] this[Int32 colIndex] {
            get {
                var colsValue = this.array.GetUpperBound(0) + 1 - this.array.GetLowerBound(0);
                var rowArray = new Double[colsValue];

                for (var i = 0; i < colsValue; i++) {
                    rowArray[i] = this.array[i][colIndex];
                }

                return rowArray;
            }
        }

        public StrategiesTable(Double[][] array, Direction direction) {
            this.array = array;
            this.direction = direction;
        }

    }
}
