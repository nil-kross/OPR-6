using Lomtseu.GamesTheory;
using Lomtseu.Tables;
using System;

namespace Lomtseu {
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

            if (matrix == null) {
                throw new ArgumentNullException("Double[][] 'array' was null!");
            } else {
                var lengthValue = matrix.RowsCount; // TO DO
                var isDominatedArray = new Boolean[lengthValue];

                for (Byte f = 0; f < lengthValue - 1; f++) {
                    for (Byte s = (Byte)(f + 1); s < lengthValue; s++) {
                        var dominatesCountValue = 0;

                        for (Byte k = 0; k < 2; k++) {
                            var one = matrix[k, f];
                            var other = matrix[k, s];

                            dominatesCountValue += (one <= other) ? 1 : 0;
                        }

                        isDominatedArray[f] = isDominatedArray[f] || dominatesCountValue == 2;
                        isDominatedArray[s] = isDominatedArray[s] || dominatesCountValue == 0;
                    }
                }

                {
                    var newLengthValue = 0;

                    for (var p = 0; p < matrix.RowsCount; p++) { // TO DO
                        newLengthValue += !isDominatedArray[p] ? 1 : 0;
                    }

                    paretoArray = new Double[2][];
                    for (var c = 0; c < 2; c++) {
                        paretoArray[c] = new Double[newLengthValue];
                    }

                    {
                        var i = 0;

                        for (Byte r = 0; r < matrix.RowsCount; r++) {
                            if (!isDominatedArray[r]) {
                                for (Byte c = 0; c < 2; c++) {
                                    paretoArray[c][i] = matrix[c, r];
                                }
                                i++;
                            }
                        }
                    }
                }

                model.Array = paretoArray;
                model.Table = new Table(2, paretoArray[0].Length).ForEach(
                    (cell, row, col) => new TextCell(paretoArray[row][col].ToString())
                );
            }

            return model;
        }
    }
}
