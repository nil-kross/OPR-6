using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lomtseu.Tables
{
    public static class WindowsFormsExtension
    {
        public static void Load(this DataGridView grid, Table table)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();

            for (var c = 0; c < table.ColsAmount; c++)
            {
                var cell = new DataGridViewButtonCell() {
                    Value = ""
                };
                var col = new DataGridViewColumn(cell);

                grid.Columns.Add(col);
            }

            for (var r = 0; r < table.RowsAmount; r++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (var c = 0; c < table.ColsAmount; c++)
                {
                    var tableCell = table[r, c];
                    var cell = new DataGridViewTextBoxCell() {
                        Value = tableCell.Value
                    };

                    row.Cells.Add(cell);
                }

                grid.Rows.Add(row);
            }
        }
    }
}