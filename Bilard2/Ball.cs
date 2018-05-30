using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilard2
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int VX { get; set; }
        public int VY { get; set; }
        public int R { get; set; }

        public int D { get { return 2 * R; } }

        public int NVX { get; set; }
        public int NVY { get; set; }
        public int Mass { get; set; }
    }
}
