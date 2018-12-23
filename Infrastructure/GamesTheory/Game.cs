using Lomtseu.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.GamesTheory {
    public class Game {
        private Double[,] array;
        private GameModes gameMode;

        protected Int32 M {
            get
            {
                return 0;
            }
        }
        protected Int32 N {
            get {
                return 2;
            }
        }

        public Game(Double[,] array) {
            this.array = array;

            if (array.Length == 2) {
                this.gameMode = GameModes.MPerTwo;
            } else if (array.Length == 2) {
                this.gameMode = GameModes.TwoPerM;
            } else {
                throw new Exception();
            }
        }

        public Table DeletePareto() {
            Table table = null;

            {
                Int32 count = this.M;
                bool[] isDominated = new bool[count];

                if (this.gameMode == GameModes.MPerTwo) {
                } else {
                }
            }

            return table;
        }
    }
}