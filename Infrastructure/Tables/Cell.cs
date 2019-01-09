using System;
using System.Drawing;

namespace Lomtseu.Tables {
    public abstract class Cell {
        public Color Fore { get; set; }
        public Color Back { get; set; }
        public Int32 Row { get; set; }
        public Int32 Col { get; set; }
        public abstract String Value { get; set; }
        public abstract String Default { get; set; }

        public override String ToString() {
            return this.Value.ToString();
        }
    }
}
