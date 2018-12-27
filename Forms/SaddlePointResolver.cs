using Lomtseu.GamesTheory;
using Lomtseu.Tables;
using System;
using System.Drawing;
using System.Linq;

namespace Lomtseu {
    public class SaddlePointModel {
        public Boolean IsFounded { get; set; }
        public Table Table { get; set; }
    }

    public interface ISaddlePointResolver {
        SaddlePointModel Resolve(Matrix matrix);
    }

    public class SaddlePointResolver : ISaddlePointResolver {
        public SaddlePointModel Resolve(Matrix matrix) {
            var model = new SaddlePointModel() { IsFounded = false };

            if (matrix != null) {
                Table saddleTable = null;
                var rowsValue = matrix.RowsCount;
                var colsValue = matrix.ColsCount;
                Point? point = null;
                Double[] minByRowArray = new Double[rowsValue];
                Double[] maxByColArray = new Double[colsValue];

                for (var r = 0; r < rowsValue; r++) {
                    minByRowArray[r] = matrix.Rows[r].Value.Min();
                }
                for (Byte c = 0; c < colsValue; c++) {
                    var maxValue = Double.MinValue;

                    for (Byte r = 0; r < rowsValue; r++) {
                        maxValue = Math.Max(matrix[r, c], maxValue);
                    }
                    maxByColArray[c] = maxValue;
                }

                {
                    Table table = null;
                    var maxByMinByRowValue = minByRowArray.Max();
                    var minByMaxByColValue = maxByColArray.Min();
                    var maxByRowsCountValue = 0;
                    var maxByRowsIndexValue = -1;
                    var minByColsCountValue = 0;
                    var minByColsIndexValue = -1;

                    {
                        var i = 0;

                        foreach (var minByRowValue in minByRowArray) {
                            if (minByRowValue == maxByMinByRowValue) {
                                maxByRowsCountValue++;
                                maxByRowsIndexValue = i;
                            }
                            i++;
                        }
                    }
                    {
                        var i = 0;

                        foreach (var maxByColValue in maxByColArray) {
                            if (maxByColValue == minByMaxByColValue) {
                                minByColsCountValue++;
                                minByColsIndexValue = i;
                            }
                            i++;
                        }
                    }

                    table = new Table(rowsValue + 1, colsValue + 1).ForEach(c => new TextCell(""));
                    if (maxByRowsCountValue == 1 && minByColsCountValue == 1) {
                        point = new Point(minByColsIndexValue, maxByRowsIndexValue);
                    }

                    for (Byte r = 0; r < rowsValue; r++) {
                        for (Byte c = 0; c < colsValue; c++) {
                            var isSaddle = (point != null && (r == point.Value.Y && c == point.Value.X));

                            table[r, c] = new TextCell(matrix[r, c].ToString()) {
                                Fore = isSaddle ? Color.DarkGreen : Color.Black,
                            };
                        }
                    }
                    for (var r = 0; r < rowsValue; r++) {
                        var isMaxMin = minByRowArray[r] == maxByMinByRowValue;
                        var textString = String.Format("{0}{1}", minByRowArray[r], isMaxMin ? " *" : "");

                        table[r, colsValue] = new TextCell(textString) {
                            Fore = isMaxMin ? Color.Red : Color.Black
                        };
                    }
                    for (var c = 0; c < colsValue; c++) {
                        var isMinMax = maxByColArray[c] == minByMaxByColValue;
                        var textString = String.Format("{0}{1}", maxByColArray[c], isMinMax ? " *" : "");

                        table[rowsValue, c] = new TextCell(textString) {
                            Fore = isMinMax ? Color.Red : Color.Black
                        };
                    }

                    model.IsFounded = point != null;
                    model.Table = table;
                }
            } else {
                throw new ArgumentNullException("Matrix 'matrix' was null!");
            }

            return model;
        }
    }
}
