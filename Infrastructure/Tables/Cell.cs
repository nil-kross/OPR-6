using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomtseu.Tables
{
    public abstract class Cell
    {
        public Color Fore { get; set; }
        public Color Back { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public abstract string Value { get; set; }
        public abstract string Default { get; set; }

        public override String ToString()
        {
            return this.Value.ToString();
        }
    }
}