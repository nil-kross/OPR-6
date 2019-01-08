using Lomtseu.GamesTheory;
using Lomtseu.Tables;
using System;
using System.Drawing;

namespace Lomtseu.Modules {
    public class ClearCostModel {
        public Table Table { get; set; }
        public Double? ClearCost { get; set; }
    }

    public interface IClearCostResolver {
        ClearCostModel Solve(Matrix matrix);
    }

    public class ClearCostResolver : IClearCostResolver {
        private static Byte extensionValue = 2;

        public ClearCostModel Solve(Matrix matrix) {
            Func<Int32, Int32> swap = (number) => number == 0 ? 1 : 0;
            Table table = null;
            Nullable<Double> clearCostValue = null;

            if (matrix != null && matrix.RowsCount == 2 && matrix.ColsCount == 2) {
                var costsArray = new Double[4];
                var k1 = matrix[0, 0];
                var k2 = matrix[1, 0];
                var s1 = matrix[0, 1];
                var s2 = matrix[1, 1];
                var ra = new Double[2];
                var ca = new Double[2];

                for (Byte p = 0; p < 2; p++) {
                    ra[p] = Math.Abs(matrix[0, p] - matrix[1, p]);
                    ca[p] = Math.Abs(matrix[p, 0] - matrix[p, 1]);
                }

                table = new Table(2 + extensionValue, 2 + extensionValue)
                    .ForEach((cell, r, c) => new TextCell(
                        r < 2 && c < 2
                            ? matrix[(Byte)r, (Byte)c].ToString()
                            : ""
                    ));
                for (var p = 0; p < 2; p++) {
                    var equations = new String[2];
                    var strings = new String[2];
                    var summary = new Double[2];
                    var divider = new Double[2];

                    for (var k = 0; k < 2; k++) {
                        strings[0] += String.Format("{0}x{1}{2}", ca[k].ToString(), matrix[(Byte)swap(k), (Byte)p], k == 0 ? " + " : "");
                        summary[0] += ca[k] * matrix[(Byte)swap(k), (Byte)p];

                        strings[1] += String.Format("{0}x{1}{2}", ra[k].ToString(), matrix[(Byte)p, (Byte)swap(k)], k == 0 ? " + " : "");
                        summary[1] += ra[k] * matrix[(Byte)p, (Byte)swap(k)];

                        divider[0] += ca[k];
                        divider[1] += ra[k];
                    }

                    for (var k = 0; k < 2; k++) {
                        var i = p * 2 + k;
                        var costValue  = summary[k] / divider[k];

                        equations[k] = String.Format("({0})/{2}={1}/{2}={3:G3}", strings[k], summary[k], divider[k], costValue);
                        costsArray[i] = costValue;
                    }

                    table[p, 3] = new TextCell(equations[0]) { Back = Color.Gainsboro };
                    table[3, p] = new TextCell(equations[1]) { Back = Color.Gainsboro };

                    table[p, 2] = new TextCell(ca[p].ToString()) { Back = Color.Gainsboro };
                    table[2, p] = new TextCell(ra[p].ToString()) { Back = Color.Gainsboro };
                }

                {
                    var isEquals = true;

                    for (var i = 0; i < 4; i++) {
                        for (var j = 0; j < 4; j++) {
                            if (i != j) {
                                if (costsArray[i] != costsArray[j]) {
                                    isEquals = false;
                                }
                            }
                        }
                    }

                    if (isEquals) {
                        table[3, 3] = new TextCell(String.Format("Цена игры: {0:G3}", costsArray[0])) { Back = Color.YellowGreen };
                    }
                }
            }

            return new ClearCostModel() {
                Table = table,
                ClearCost = clearCostValue
            };
        }
    }
}
