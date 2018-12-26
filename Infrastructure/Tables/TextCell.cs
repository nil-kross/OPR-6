using System;

namespace Lomtseu.Tables {
    public class TextCell : Cell {
        private String textString = null;
        private String defaultString = null;

        public override String Value {
            get {
                return this.textString ?? this.defaultString;
            }
            set { this.textString = value; }
        }
        public override String Default {
            get { return this.defaultString; }
            set { this.defaultString = value; }
        }

        public TextCell(String @default = "") : base() {
            this.defaultString = @default;
        }

        public override String ToString() {
            return this.textString;
        }
    }
}
