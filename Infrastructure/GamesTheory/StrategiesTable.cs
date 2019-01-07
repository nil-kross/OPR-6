using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.GamesTheory {
    public class StrategiesTable {
        private Double[][] arrayValue;
        private Contour contourValue;

        public Double[][] Strategies {
            get { return this.arrayValue; }
        }
        public Contour Contour {
            get { return this.contourValue; }
        }
        public Int32 Length {
            get { return this.arrayValue[0].Length; }
        }
        public Double[] this[Int32 colIndex] {
            get {
                var colsValue = this.arrayValue.GetUpperBound(0) + 1 - this.arrayValue.GetLowerBound(0);
                var rowArray = new Double[colsValue];

                for (var i = 0; i < colsValue; i++) {
                    rowArray[i] = this.arrayValue[i][colIndex];
                }

                return rowArray;
            }
        }

        public StrategiesTable(Double[][] array, Contour direction) {
            this.arrayValue = array;
            this.contourValue = direction;
        }

    }
}
