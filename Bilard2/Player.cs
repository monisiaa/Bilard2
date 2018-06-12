using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bilard2
{
    public class Player
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;

        public Player addPlayer(int x1, int y1, int x2, int y2)
        {
            Player p = new Player()
            {
                x1 = x1,
                y1 = y1,
                x2 = x2,
                y2 = y2
            };
            return p;
        }
    }
}
