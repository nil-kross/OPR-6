using Lomtseu.GamesTheory;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Lomtseu {
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

            var a = GraphicSolver.Solve(this.strategiesTable);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);
            for (var r = 0; r < this.strategiesTable.Length; r++) {
                maxValue = Math.Max(maxValue, this.strategiesTable[r].Max());
                minValue = Math.Min(minValue, this.strategiesTable[r].Min());
            }

            {
                Double gapValue = 20.0 / 100;
                var deltaValue = Math.Abs(maxValue - minValue);
                var hieghtValue = this.ClientSize.Height / deltaValue;

                // Соединительные линии
                {
                    Pen pen = new Pen(Color.Black, 1);

                    for (var r = 0; r < this.strategiesTable.Strategies[0].Length; r++) {
                        var leftValue = this.ClientSize.Height - (this.strategiesTable.Strategies[0][r] - minValue) * hieghtValue;
                        var rightValue = this.ClientSize.Height - (this.strategiesTable.Strategies[1][r] - minValue) * hieghtValue;

                        graphics.DrawLine(pen, (Int32)(this.ClientSize.Width * (0 + gapValue)), (Int32)leftValue, (Int32)(this.ClientSize.Width * (1 - gapValue)), (Int32)rightValue);
                    }
                }
                // Вертикальные линии
                {
                    Pen pen = new Pen(Color.Black, 3);

                    graphics.DrawLine(pen, (Int32)(this.ClientSize.Width * (0 + gapValue)), 0, (Int32)(this.ClientSize.Width * (0 + gapValue)), this.ClientSize.Height);
                    graphics.DrawLine(pen, (Int32)(this.ClientSize.Width * (1 - gapValue)), 0, (Int32)(this.ClientSize.Width * (1 - gapValue)), this.ClientSize.Height);
                }
                // Горизонтальные штрихи
                {
                    Pen pen = new Pen(Color.Black, 1);

                    for (var i = 0; i < deltaValue; i++) {
                        var currHeightValue = this.ClientSize.Height - i * hieghtValue;

                        graphics.DrawLine(pen, (Int32)(this.ClientSize.Width * (1 - gapValue)), (Int32)currHeightValue, (Int32)(this.ClientSize.Width * (1 - gapValue)) + 5, (Int32)currHeightValue);
                        graphics.DrawLine(pen, (Int32)(this.ClientSize.Width * (0 + gapValue)), (Int32)currHeightValue, (Int32)(this.ClientSize.Width * (0 + gapValue)) - 5, (Int32)currHeightValue);

                        Action<Int32, Sides> mark = (Int32 c, Sides side) => {
                            var number = i + minValue;

                            if (this.strategiesTable.Strategies[c].Contains(number)) {
                                var markString = number.ToString();
                                var markFont = new Font("Times New Roman", 14.0f);
                                SizeF markSize = graphics.MeasureString(markString, markFont);
                                var markX = this.ClientSize.Width * (side == Sides.Right ? 1 - gapValue : 0 + gapValue);

                                graphics.DrawString(markString, markFont, new SolidBrush(Color.Red), new PointF((Single)(markX - markSize.Width / 2 + (side == Sides.Right ? +1 : -1) * (markSize.Width * 1.0)), (Single)(currHeightValue - markSize.Height / 2)));
                            }
                        };

                        mark(1, Sides.Right);
                        mark(0, Sides.Left);
                    }
                }
            }
        }

        private void OnGraphFormResize(Object sender, EventArgs e) {
            this.ResizeLayout();
        }

        private void OnPictureBoxClick(Object sender, EventArgs e) {
            this.ResizeLayout();
        }
    }
}