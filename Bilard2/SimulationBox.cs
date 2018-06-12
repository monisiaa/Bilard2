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
        /*public Player addPlayer(int x1, int y1, int x2, int y2)
        {
            Player p = new Player()
            {
                x1 = x1,
                y1 = y1,
                x2 = x2,
                y2 = y2
            };
            return p;
        }*/
    }
}
