using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;

namespace Bilard2
{
    public class SimulationBoxOld
    {

        public const int radius = 12;

        List<Sphere> ballList = new List<Sphere>();

        public Sphere addBall(int X, int Y, int VX, int VY)
        {
            Sphere b = new Sphere()
            {
                X = X,
                Y = Y,
                VX = VX,
                VY = VY,
                R = radius,
                Mass = 1
            };
            ballList.Add(b);
            return b;
        }

        public void removeBall(Sphere sphere)
        {
            balls.Remove(sphere);
        }

        public void fromTheWalls(int tableX, int tableY, int tableW, int tableH)
        {
            foreach (var b in ballList)
            {
                if (b.X < tableX || b.X + b.D > tableW + tableX)
                {
                    b.VX = -b.VX;
                }
                if (b.Y < tableY || b.Y + b.D > tableH + tableY)
                {
                    b.VY = -b.VY;
                }

                b.X = b.X + b.VX;
                b.Y = b.Y + b.VY;

            }
        }



       public void Collission()
        {
            for (int i = 0; i < ballList.Count(); i++)
            {
                for (int j = i + 1; j < ballList.Count(); j++)
                {
                    var b1 = ballList[i];
                    var b2 = ballList[j];

                    if (Math.Sqrt(Math.Pow(b1.X - b2.X, 2) + Math.Pow(b1.Y - b2.Y, 2)) - (b1.R + b2.R) < 0)
                    {

                        // http://www.gamasutra.com/view/feature/131424/pool_hall_lessons_fast_accurate_.php?page=3

                        Vector pos1 = new Vector(b1.X, b1.Y);
                        Vector pos2 = new Vector(b2.X, b2.Y);

                        Vector N = pos1 - pos2;
                        N.Normalize();

                        Vector v1 = new Vector(b1.VX, b1.VY);
                        Vector v2 = new Vector(b2.VX, b2.VY);

                        // a1 =  v1 . N
                        double a1 = Vector.Multiply(v1, N);

                        // a2 = v2 . N
                        double a2 = Vector.Multiply(v2, N);

                        double m1 = b1.Mass;
                        double m2 = b2.Mass;

                        double p = 2 * (a1 - a2) / (m1 + m2);

                        Vector newV1 = v1 - p * m2 * N;
                        Vector newV2 = v2 + p * m1 * N;

                        b1.VX = newV1.X;
                        b1.VY = newV1.Y;
                        b2.VX = newV2.X;
                        b2.VY = newV2.Y;
                    }
                }
            }
        }

        public List<Sphere> balls
        {
            get
            {
                return ballList;
            }
        }
    }
}
