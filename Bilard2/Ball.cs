using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilard2
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VX { get; set; }
        public double VY { get; set; }
        public double R { get; set; }

        public double D { get { return 2 * R; } }
         
        public double NVX { get; set; }
        public double NVY { get; set; }
        public double Mass { get; set; }
    }
}
