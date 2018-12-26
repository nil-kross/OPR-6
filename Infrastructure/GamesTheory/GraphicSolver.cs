using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.GamesTheory {
    public static class GraphicSolver {
        public static Object Solve(StrategiesTable strategiesTable) {
            LinearEquation[] equationsArray = new LinearEquation[strategiesTable.Length];

            for (var i = 0; i < strategiesTable.Length; i++) {
                var rowArray = strategiesTable[i];
                var leftPoint = new PointF(0, (Single)rowArray[0]);
                var rightPoint = new PointF(1, (Single)rowArray[1]);
                var equation = LinearEquation.Create(leftPoint, rightPoint);

                equationsArray[i] = equation;
            }

            return equationsArray;
        }
    }
}
