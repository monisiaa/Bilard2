using System.Drawing;
using System.Windows;

namespace Bilard2
{
    public class Ball
    {
        public Sphere Sphere { get; set; }
        public SolidBrush Color { get; set; }

        public double X { get { return Center.X - R; } }
        public double Y { get { return Center.Y - R; } }
        public double R { get; set; } = 12;

        public double CenterX { get { return X + R; } }
        public double CenterY { get { return Y + R; } }
        public double Mass => 1;

        public Vector Velocity { get; set; } = new Vector(0, 0);

        public Vector Center { get; set; }
    
        public bool IsVisible { get; set; } = true;
        public BallType Type { get; set; }

        public void Draw(Graphics graphics)
        {
            if (IsVisible)
            {
                graphics.FillEllipse(Color, (float)X, (float)Y, (float)R * 2, (float)R * 2);
            }
        }
    }

    public enum BallType
    {
        White,
        Color,
        Black
    }
}
