using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lomtseu
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.ResizeLayout();
        }

        protected void ResizeLayout()
        {
            var height = 40;

            this.grid.Location = new Point(0, height);
            this.grid.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - height);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.ResizeLayout();
        }
    }
}