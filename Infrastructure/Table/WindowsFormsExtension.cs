using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lomtseu.Table
{
    public static class WindowsFormsExtension
    {
        public static void Load(this DataGridView grid, Table table)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                grid.Rows.Remove(row);
            }

        }
    }
}