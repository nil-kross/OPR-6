using System;
using System.Drawing;

namespace Lomtseu.GamesTheory {

    public class LinearEquation {
        public Double K { get; protected set; }
        public Double B { get; protected set; }
        public Double this[Double x] {
            get { return this.K * x + this.B; }
        }

        public LinearEquation(Double k, Double b) {
            this.K = k;
            this.B = b;
        }

        public static LinearEquation Create(PointF first, PointF second) {
            Double k = (first.Y - second.Y) / (first.X - second.X);
            Double b = (first.Y + second.Y - k * (first.X + second.X)) / 2;

            return new LinearEquation(k, b);
        }

        public override String ToString() {
            return String.Format("y={0:F2}x {2} {1:F2}", this.K, Math.Abs(this.B), (this.B >= 0 ? '+' : '-'));
        }

        public static PointF CrossPoint(LinearEquation first, LinearEquation second) {
            PointF crossPoint;

            {
                var x = (second.B - first.B) / (first.K - second.K);
                var y = first[x];

                crossPoint = new PointF((Single)x, (Single)y);
            }

            return crossPoint;
        }
    }
}
