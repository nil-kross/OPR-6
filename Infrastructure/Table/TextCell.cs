﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.Table
{
    public class TextCell : Cell
    {
        private string textString = null;
        private string defaultString = null;

        public override string Value {
            get { return this.textString; }
            set { this.textString = value; }
        }
        public override string Default {
            get { return defaultString; }
            set { this.defaultString = value; }
        }
        public TextCell(string @default = "") : base()
        {
            this.defaultString = @default;
        }

        public override string ToString()
        {
            return this.textString;
        }
    }
}