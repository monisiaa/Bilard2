using System.Drawing;

namespace Bilard2
{
    class Ball
    {
        public Sphere Sphere { get; set; }
        public SolidBrush Color { get; set; }

        public double X { get { return Sphere.X; } }
        public double Y { get { return Sphere.Y; } }
        public double R { get { return Sphere.R / 2; } }

        public bool IsVisible { get; set; } = true;
        public BallType Type { get; set; }
    }

    enum BallType
    {
        White,
        Color,
        Black
    }
}
