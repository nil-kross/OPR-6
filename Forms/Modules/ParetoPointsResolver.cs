using Lomtseu.GamesTheory;
using Lomtseu.Tables;
using System;
using System.Drawing;

namespace Lomtseu {
    public enum Direction {
        Max = 0,
        Min = 1
    }

    public class ParetoPointsModel {
        public Table Table { get; set; }
        public Double[][] Array { get; set; }
    }

    public interface IParetoPointsResolver {
        ParetoPointsModel Resolve(Matrix matrix);
    }

    public class ParetoPointsResolver : IParetoPointsResolver {
        public ParetoPointsModel Resolve(Matrix matrix) {
            var model = new ParetoPointsModel() { Table = null };
            Double[][] paretoArray = null;

            {
                var colsValue = matrix.ColsCount;
                var rowsValue = matrix.RowsCount;
                var isColDominatedArray = new Boolean[colsValue];
                var isRowDominatedArray = new Boolean[rowsValue];


                for (Byte f = 0; f < rowsValue - 1; f++) {
                    for (Byte s = (Byte)(f + 1); s < rowsValue; s++) {
                        var dominatesCountValue = 0;

                        for (Byte c = 0; c < colsValue; c++) {
                            var one = matrix[f, c];
                            var other = matrix[s, c];

                            dominatesCountValue += IsBetter(one, other, Direction.Max) ? 1 : 0;
                        }

                        isRowDominatedArray[f] = isRowDominatedArray[f] || dominatesCountValue == 0;
                        isRowDominatedArray[s] = isRowDominatedArray[s] || dominatesCountValue == colsValue;
                    }
                }

                for (Byte f = 0; f < colsValue - 1; f++) {
                    for (Byte s = (Byte)(f + 1); s < colsValue; s++) {
                        var dominatesCountValue = 0;

                        for (Byte r = 0; r < rowsValue; r++) {
                            var one = matrix[r, f];
                            var other = matrix[r, s];

                            dominatesCountValue += IsBetter(one, other, Direction.Min) ? 1 : 0;
                        }

                        isColDominatedArray[f] = isColDominatedArray[f] || dominatesCountValue == 0;
                        isColDominatedArray[s] = isColDominatedArray[s] || dominatesCountValue == rowsValue;
                    }
                }

                {
                    var newRowsValue = 0;
                    var newColsValue = 0;

                    for (var r = 0; r < rowsValue; r++) {
                        newRowsValue += isRowDominatedArray[r] ? 0 : 1;
                    }
                    for (var c = 0; c < colsValue; c++) {
                        newColsValue += isColDominatedArray[c] ? 0 : 1;
                    }

                    paretoArray = new Double[newRowsValue][];
                    for (var r = 0; r < newRowsValue; r++) {
                        paretoArray[r] = new Double[newColsValue];
                    }

                    {
                        var rowValue = 0;
                        
                        for (var r = 0; r < rowsValue; r++) {
                            if (!isRowDominatedArray[r]) {
                                var colValue = 0;

                                for (var c = 0; c < colsValue; c++) {
                                    if (!isColDominatedArray[c]) {
                                        paretoArray[rowValue][colValue] = matrix[(Byte)r, (Byte)c];
                                        colValue++;
                                    }
                                }

                                rowValue++;
                            }
                        }

                        model.Array = paretoArray;
                        model.Table = new Table(rowsValue, colsValue).ForEach(
                            (cell, row, col) => new TextCell(matrix[(Byte)row, (Byte)col].ToString()) {
                                Back = isRowDominatedArray[row] || isColDominatedArray[col] ? Color.Gainsboro : Color.White
                            }
                        );
                    }
                }
            }

            return model;
        }

        private static Boolean IsBetter(Double first, Double second, Direction direction) {
            var isBetter = false;

            if (direction == Direction.Max) {
                isBetter = first >= second;
            }
            if (direction == Direction.Min) {
                isBetter = second >= first;
            }

            return isBetter;
        }
    }
}
