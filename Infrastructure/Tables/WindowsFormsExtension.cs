using System;
using System.Windows.Forms;

namespace Lomtseu.Tables {
    public static class WindowsFormsExtension {
        public static void Load(this DataGridView grid, Table table) {
            if (grid == null) {
                throw new ArgumentNullException("DataGridView 'grid' was null!");
            }
            if (table == null) {
                throw new ArgumentNullException("Table 'table' was null!");
            }

            grid.Rows.Clear();
            grid.Columns.Clear();

            for (var c = 0; c < table.ColsAmount; c++) {
                var cell = new DataGridViewTextBoxCell() {
                    Value = ""
                };
                var col = new DataGridViewColumn(cell);

                col.MinimumWidth = 50;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                grid.Columns.Add(col);
            }

            for (var r = 0; r < table.RowsAmount; r++) {
                DataGridViewRow row = new DataGridViewRow();

                for (var c = 0; c < table.ColsAmount; c++) {
                    Cell tableCell = table[r, c];
                    var cell = new DataGridViewTextBoxCell() {
                        Value = tableCell.Value,
                        Style = new DataGridViewCellStyle() {
                            ForeColor = tableCell.Fore,
                            BackColor = tableCell.Back,
                            WrapMode = DataGridViewTriState.False,
                            Alignment = DataGridViewContentAlignment.MiddleRight
                        }
                    };

                    row.Cells.Add(cell);
                }

                grid.Rows.Add(row);
            }
        }

        public static Table Save(this DataGridView grid) {
            Table table = null;

            if (grid != null) {
                table = new Table(grid.RowCount - 1, grid.ColumnCount).ForEach((cell) => new TextCell());

                {
                    var r = 0;

                    foreach (DataGridViewRow row in grid.Rows) {
                        if (r < grid.RowCount - 1) {
                            var c = 0;

                            foreach (DataGridViewTextBoxCell col in row.Cells) {
                                table[r, c] = new TextCell(col.Value as String);

                                c++;
                            }
                        }

                        r++;
                    }
                }
            }

            return table;
        }
    }
}
