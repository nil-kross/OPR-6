using System;
using System.Windows.Forms;

namespace Lomtseu {

    public interface IArrayFromGridParser {
        Double[][] Parse(DataGridView grid);
    }

    public class ArrayFromGridParser : IArrayFromGridParser {
        public Double[][] Parse(DataGridView grid) {
            Double[][] array = null;

            if (grid != null) {
                var rowsValue = grid.Rows.Count - 1;
                var colsValue = grid.Columns.Count;

                array = new Double[rowsValue][];
                for (var r = 0; r < rowsValue; r++) {
                    array[r] = new Double[colsValue];
                }
                {
                    var r = 0;

                    foreach (DataGridViewRow row in grid.Rows) {
                        if (r < grid.RowCount - 1) {
                            var c = 0;

                            if (r < grid.Rows.Count - 1) {
                                foreach (DataGridViewTextBoxCell col in row.Cells) {
                                    array[r][c] = Double.Parse(col.Value as String);

                                    c++;
                                }
                            }
                        }

                        r++;
                    }
                }
            } else {
                throw new ArgumentNullException();
            }

            return array;
        }
    }
}
