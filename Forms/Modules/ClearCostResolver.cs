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
        public ClearCostModel Solve(Matrix matrix) {
            Func<Int32, Int32> swap = (number) => number == 0 ? 1 : 0;
            Table table = null;
            Nullable<Double> clearCostValue = null;

            if (matrix != null) {
                if (matrix.RowsCount == 2 && matrix.ColsCount == 2) {
                    Byte extensionValue = 2;
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
                            var costValue = summary[k] / divider[k];

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
                            table[3, 3] = new TextCell(String.Format("{0:G3}", costsArray[0])) { Back = Color.YellowGreen };
                        }
                    }
                }
                if (matrix.RowsCount == 3 && matrix.ColsCount == 3) {
                    Byte extensionValue = 4;
                    var ra = new Double[2][];
                    var ca = new Double[2][];
                    var determinants = new Double[2][];
                    var strings = new String[2][];
                    var summary = new Double[2][];
                    var equations = new String[2][];
                    var costs = new Double[2][];

                    for (Byte i = 0; i < 2; i++) {
                        ra[i] = new Double[3];
                        ca[i] = new double[3];

                        for (Byte j = 0; j < 3; j++) {
                            ra[i][j] = matrix[(Byte)(i + 0), j] - matrix[(Byte)(i + 1), j];
                            ca[i][j] = matrix[j, (Byte)(i + 0)] - matrix[j, (Byte)(i + 1)];
                        }

                        determinants[i] = new Double[3];
                        strings[i] = new String[3];
                    }

                    for (var k = 0; k < 3; k++) {
                        var l = k == 0
                                    ? 1
                                    : 0;
                        var r = k == 2
                                    ? 1
                                    : 2;

                        determinants[0][k] = Math.Abs(ra[0][l] * ra[1][r] - ra[0][r] * ra[1][l]);
                        determinants[1][k] = Math.Abs(ca[0][l] * ca[1][r] - ca[0][r] * ca[1][l]);

                        strings[0][k] = String.Format("{0}*{1} - {2} * {3} = {4}", ra[0][l], ra[1][r], ra[0][r], ra[1][l], determinants[0][k]);
                        strings[1][k] = String.Format("{0}*{1} - {2} * {3} = {4}", ca[0][l], ca[1][r], ca[0][r], ca[1][l], determinants[1][k]);
                    }

                    table = new Table(3 + extensionValue, 3 + extensionValue)
                        .ForEach((cell, r, c) => new TextCell(
                            r < 3 && c < 3
                                ? matrix[(Byte)r, (Byte)c].ToString()
                                : ""
                        ) { Back = r < 3 && c < 3 ? Color.White : Color.Gainsboro });
                    for (var i = 0; i < 3; i++) {
                        for (var k = 0; k < 2; k++) {
                            table[i, 3 + k] = new TextCell(ca[k][i].ToString()) { Back = Color.Wheat };
                            table[3 + k, i] = new TextCell(ra[k][i].ToString()) { Back = Color.Wheat };
                        }

                        table[i, 3 + 2] = new TextCell(strings[1][i]) { Back = Color.Wheat };
                        table[3 + 2, i] = new TextCell(strings[0][i]) { Back = Color.Wheat };
                    }

                    for (var s = 0; s < 2; s++) {
                        summary[s] = new Double[3];
                        equations[s] = new String[3];
                        costs[s] = new Double[3];
                    }
                    for (var i = 0; i < 3; i++) {
                        var divider = new Double[2];

                        for (var j = 0; j < 3; j++) {
                            equations[0][i] += String.Format("{0}{1}x{2}", j == 0 ? "" : " + ", determinants[0][j], matrix[(Byte)i, (Byte)j]);
                            summary[0][i] += determinants[0][j] * matrix[(Byte)i, (Byte)j];
                            divider[0] += determinants[0][j];

                            equations[1][i] += String.Format("{0}{1}x{2}", j == 0 ? "" : " + ", determinants[1][j], matrix[(Byte)j, (Byte)i]);
                            summary[1][i] += determinants[1][j] * matrix[(Byte)j, (Byte)i];
                            divider[1] += determinants[1][j];
                        }
                        costs[0][i] = summary[0][i] / divider[0];
                        costs[1][i] = summary[1][i] / divider[1];

                        table[i, 3 + 2 + 1] = new TextCell(String.Format("({3})/{1}={0}/{1}={2:G3}", summary[1][i], divider[1], costs[1][i], equations[1][i]));
                        table[3 + 2 + 1, i] = new TextCell(String.Format("({3})/{1}={0}/{1}={2:G3}", summary[0][i], divider[0], costs[0][i], equations[0][i]));
                    }
                    {
                        var isEquals = true;

                        for (var p = 0; p < 2; p++)
                        {
                            for (var k = 0; k < 3; k++)
                            {
                                for (var i = 0; i < 2; i++)
                                {
                                    for (var j = 0; j < 3; j++)
                                    {
                                        if (p != i && k != j && costs[p][k] != costs[i][j])
                                        {
                                            isEquals = false;
                                        }
                                    }
                                }
                            }
                        }

                        if (isEquals)
                        {
                            table[6, 6] = new TextCell(String.Format("{0:G3}", costs[0][0])) { Back = Color.YellowGreen };
                        }
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
