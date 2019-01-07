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

            var a = GraphicSolver.Solve(this.strategiesTable);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);
            for (var r = 0; r < this.strategiesTable.Length; r++) {
                maxValue = Math.Max(maxValue, this.strategiesTable[r].Max());
                minValue = Math.Min(minValue, this.strategiesTable[r].Min());
            }

            {
                Double gapValue = 0.15;
                Action<Pen, Single, Single, Single, Single> drawLine = (pen, x0, y0, x1, y1) => {
                    Func<Single, Single> calcX = (x) => (Single)(this.ClientSize.Width * (gapValue + x * (1 - 2 * gapValue)));

                    graphics.DrawLine(pen, (Int32)(calcX(x0)), (Int32)y0, (Int32)(calcX(x1)), (Int32)y1);
                };
                var deltaValue = Math.Abs(maxValue - minValue);
                var hieghtValue = this.ClientSize.Height / deltaValue;

                // Соединительные линии
                {
                    Pen pen = new Pen(Color.Black, 1);

                    for (var r = 0; r < this.strategiesTable.Strategies[0].Length; r++) {
                        var leftValue = this.ClientSize.Height - (this.strategiesTable.Strategies[0][r] - minValue) * hieghtValue;
                        var rightValue = this.ClientSize.Height - (this.strategiesTable.Strategies[1][r] - minValue) * hieghtValue;

                        drawLine(pen, 0, (Single)leftValue, 1, (Single)rightValue);
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
                // Строим контур:
                {
                    var lengthValue = this.strategiesTable.Strategies[0].Length;
                    PointF[] array = new PointF[lengthValue];
                    Pen pen = new Pen(Color.Red, 3);

                    for (var i = 0; i < lengthValue; i++) {
                        array[i] = new PointF((Single)this.strategiesTable.Strategies[0][i], (Single)this.strategiesTable.Strategies[1][i]);
                    }

                    {
                        var usedPointsSet = new HashSet<PointF>();
                        var candidatePointsList = new List<PointF>();
                        var sequencePointsList = new List<PointF>();
                        var isCanDo = false;
                        PointF currPointValue;
                        Func<PointF, LinearEquation> getEquationByPoint = (index) => {
                            return LinearEquation.Create(new PointF(0, (Single)index.X), new PointF(1, (Single)index.Y));
                        };

                        {
                            var cornerLeftPoints = array.Where((point) => point.X == (this.strategiesTable.Contour == Contour.Bottom
                                                                                        ? this.strategiesTable.Strategies[0].Min()
                                                                                        : this.strategiesTable.Strategies[0].Max()));

                            currPointValue = cornerLeftPoints.First();
                            for (var i = 0; i < cornerLeftPoints.Count(); i++) {
                                var pointValue = cornerLeftPoints.ElementAt(i);

                                if (Comparator.IsBetter(pointValue.Y, currPointValue.Y, this.strategiesTable.Contour == Contour.Bottom ? Contour.Upper : Contour.Bottom)) {
                                    currPointValue = pointValue;
                                }
                            }
                        }

                        sequencePointsList.Add(new PointF(0, currPointValue.X));
                        do {
                            var optEquationOrderValue = -1;
                            var crossPointsList = new List<PointF>();
                            var currEquationValue = getEquationByPoint(currPointValue);
                            var minCrossPointValue = new PointF();

                            isCanDo = false;
                            for (var i = 0; i < lengthValue; i++) {
                                var equationValue = getEquationByPoint(array[i]);
                                var crossPointValue = LinearEquation.CrossPoint(equationValue, currEquationValue);

                                crossPointsList.Add(crossPointValue);
                            }
                            minCrossPointValue = new PointF(-1, this.strategiesTable.Contour == Contour.Bottom ? Single.MaxValue : Single.MinValue);
                            for (var i = 0; i < crossPointsList.Count; i++) {
                                var crossPointValue = crossPointsList[i];

                                if (crossPointValue != minCrossPointValue
                                    && !Double.IsNaN(crossPointValue.X)
                                    && !Double.IsNaN(crossPointValue.Y)
                                    && (crossPointValue.X > 0 && crossPointValue.X < 1)
                                    && Comparator.IsBetter(minCrossPointValue.Y, crossPointValue.Y, this.strategiesTable.Contour)
                                ) {
                                    if (!usedPointsSet.Contains(array[i])) {
                                        minCrossPointValue = crossPointValue;
                                        optEquationOrderValue = i;
                                    }
                                }
                            }

                            if (optEquationOrderValue >= 0) {
                                usedPointsSet.Add(currPointValue);
                                currPointValue = array[optEquationOrderValue];
                                candidatePointsList.Add(crossPointsList[optEquationOrderValue]);
                                sequencePointsList.Add(crossPointsList[optEquationOrderValue]);
                                if (currPointValue.Y != (this.strategiesTable.Contour == Contour.Bottom
                                                            ? this.strategiesTable.Strategies[1].Min()
                                                            : this.strategiesTable.Strategies[1].Max())
                                ) {
                                    isCanDo = true;
                                }
                            }
                        } while(isCanDo);
                        sequencePointsList.Add(new PointF(1, currPointValue.Y));

                        for (var i = 1; i < sequencePointsList.Count; i++) {
                            var prevValue = sequencePointsList[i - 1];
                            var nextValue = sequencePointsList[i + 0];
                            var leftValue = (Single)(this.ClientSize.Height - (prevValue.Y - minValue) * hieghtValue);
                            var rightValue = (Single)(this.ClientSize.Height - (nextValue.Y - minValue) * hieghtValue);

                            drawLine(pen, prevValue.X, leftValue, nextValue.X, rightValue);
                        }

                        if (candidatePointsList.Count > 0) {
                            var pointRadiusValue = 11.5f;
                            var brush = new SolidBrush(Color.Red);
                            var maxPointValue = candidatePointsList[0];
                            RectangleF pointRectangleValue;

                            for (var i = 0; i < candidatePointsList.Count; i++) {
                                var pointValue = candidatePointsList[i];

                                if (Comparator.IsBetter(pointValue.Y, maxPointValue.Y, this.strategiesTable.Contour)
                                ) {
                                    maxPointValue = pointValue;
                                }
                            }

                            {
                                var x = (Int32)(this.ClientSize.Width * (gapValue + maxPointValue.X * (1 - 2 * gapValue)) - pointRadiusValue);
                                var y = (Single)(this.ClientSize.Height - (maxPointValue.Y - minValue) * hieghtValue - pointRadiusValue);
                                pointRectangleValue = new RectangleF(x, y, pointRadiusValue * 2, pointRadiusValue * 2);
                            }

                            graphics.FillEllipse(brush, pointRectangleValue);
                        }
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