using System.Drawing;

namespace Bilard2
{
    public class Ball
    {
        public Sphere Sphere { get; set; }
        public SolidBrush Color { get; set; }

        public double X { get { return Sphere.X; } }
        public double Y { get { return Sphere.Y; } }
        public double R { get { return Sphere.R; } }

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
