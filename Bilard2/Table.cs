using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Bilard2
{
    public class Table
    {
        int X { get; set; }
        int Y { get; set; }
        int W { get; set; }
        int H { get; set; }
        int Edge { get; set; }
        Brush ColorBrush { get; set; }
        Pen EdgePen { get; set; }
        List<Hole> Holes { get; set; }

        public Table(int tableX, int tableY, int tableW, int tableH, int tableEdge, Brush colorBrush, Pen edgePen)
        {
            X = tableX;
            Y = tableY;
            W = tableW;
            H = tableH;
            Edge = tableEdge;
            ColorBrush = colorBrush;
            EdgePen = edgePen;
            Holes = new List<Hole>();
        }

        public void AddHole(int x, int y, int r)
        {
            Holes.Add(new Hole(x, y, r));
        }

        public void Draw(Graphics graphics)
        {
           
            graphics.FillRectangle(ColorBrush, X, Y, W, H);
            graphics.DrawRectangle(EdgePen, X, Y, W, H);

            foreach(var hole in Holes)
            {
                hole.Draw(graphics);
            }
        }

        public bool IsBallInsideHole(Ball ball)
        {
            return Holes.Any(h => h.IsBallInside(ball));
        }
    }

    public class Hole
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public double CollisionR { get; set; }

        public Hole(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
            CollisionR = R / 2.8;
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Black, X, Y, 2 * R, 2 * R);
            graphics.DrawEllipse(Pens.Blue, (int)(X + R - CollisionR), (int)(Y + R - CollisionR), (int)(CollisionR * 2), (int)(CollisionR * 2));
        }

        public bool IsBallInside(Ball ball)
        {
            var x = (X + R) - (ball.X + ball.R);
            var y = (Y + R) - (ball.Y + ball.R);
            var r = CollisionR;

            return Math.Sqrt(x * x + y * y) <= (r + ball.R);
        }
    }
}
