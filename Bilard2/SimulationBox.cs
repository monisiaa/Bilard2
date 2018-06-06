using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Bilard2
{
    public class SimulationBox
    {
        public const int radius = 26;

        public Ball addBall(int X, int Y, int VX, int VY)
        {
            Ball b = new Ball()
            {
                X = X,
                Y = Y,
                VX = VX,
                VY = VY,
                R = radius,
                Mass = 1
            };
            return b;
        }

    }
}
