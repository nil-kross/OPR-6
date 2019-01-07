using Lomtseu.GamesTheory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Lomtseu {
    public class Comparator {
        public static Boolean IsBetter(Double first, Double second, Contour contour) {
            Boolean isBetter = true;

            if (contour == Contour.Bottom) {
                isBetter = first > second;
            } else {
                isBetter = first < second;
            }

            return isBetter;
        }
    }

    public partial class GraphForm : Form {
        private StrategiesTable strategiesTable;

        public GraphForm(StrategiesTable strategies) {
            this.InitializeComponent();

            this.strategiesTable = strategies;

            this.ResizeLayout();
        }

        public void Draw() {
            this.DrawStrategies();
        }

        protected void ResizeLayout() {
            this.pictureBox.Location = new Point(0, 0);
            this.pictureBox.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);

            this.DrawStrategies();
        }

        private void DrawStrategies() {
            Graphics graphics = this.pictureBox.CreateGraphics();
            Double maxValue = 0;
            Double minValue = 0;
            IGraphcisSolver graphicSolver = new GraphicSolver();

            var solutionModel = graphicSolver.Solve(this.ClientSize, graphics, this.strategiesTable);
        }

        private void OnGraphFormResize(Object sender, EventArgs e) {
            this.ResizeLayout();
        }

        private void OnPictureBoxClick(Object sender, EventArgs e) {
            this.ResizeLayout();
        }
    }
}