using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lomtseu
{
    public partial class GraphForm : Form
    {
        private Double[][] strategiesArray;

        public GraphForm(Double[][] strategies)
        {
            InitializeComponent();

            this.strategiesArray = strategies;

            this.ResizeLayout();
        }

        public void Draw()
        {
            this.DrawStrategies();
        }

        protected void ResizeLayout()
        {
            this.pictureBox.Location = new Point(0, 0);
            this.pictureBox.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);

            this.DrawStrategies();
        }

        private void DrawStrategies()
        {
            Graphics graphics = this.pictureBox.CreateGraphics();
            Double maxValue = 0;
            Double minValue = 0;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);
            for (var r = 0; r < this.strategiesArray.Length; r++)
            {
                maxValue = Math.Max(maxValue, this.strategiesArray[r].Max());
                minValue = Math.Min(minValue, this.strategiesArray[r].Min());
            }

            {
                Double gapValue = 20.0 / 100;
                var deltaValue = Math.Abs(maxValue - minValue);
                var hieghtValue = this.ClientSize.Height / deltaValue;

                // Соединительные линии
                {
                    Pen pen = new Pen(Color.Black, 1);

                    for (var r = 0; r < this.strategiesArray[0].Length; r++)
                    {
                        var leftValue = this.ClientSize.Height - (this.strategiesArray[0][r] - minValue) * hieghtValue;
                        var rightValue = this.ClientSize.Height - (this.strategiesArray[1][r] - minValue) * hieghtValue;

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

                    for (var i = 0; i < deltaValue; i++)
                    {
                        var currHeightValue = i * hieghtValue;

                        graphics.DrawLine(pen, (Int32)(this.ClientSize.Width * (1 - gapValue)), (Int32)currHeightValue, (Int32)(this.ClientSize.Width * (1 - gapValue)) + 5, (Int32)currHeightValue);
                        graphics.DrawLine(pen, (Int32)(this.ClientSize.Width * (0 + gapValue)), (Int32)currHeightValue, (Int32)(this.ClientSize.Width * (0 + gapValue)) - 5, (Int32)currHeightValue);
                    }
                }
            }
        }

        private void OnGraphFormResize(object sender, EventArgs e)
        {
            this.ResizeLayout();
        }

        private void OnPictureBoxClick(Object sender, EventArgs e) {
            this.ResizeLayout();
        }
    }
}