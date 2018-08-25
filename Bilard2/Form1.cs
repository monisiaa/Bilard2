using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Bilard2
{
    public partial class Form1 : Form
    {
        #region[brush & pen]
        Pen grayPen = new Pen(Color.Gray, 20);
        Pen whitePen = new Pen(Color.White, 10);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Gold);
        SolidBrush lightyellowBrush = new SolidBrush(Color.Khaki);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush lightblueBrush = new SolidBrush(Color.DeepSkyBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush lightredBrush = new SolidBrush(Color.FromArgb(255, 70, 70));
        SolidBrush purpleBrush = new SolidBrush(Color.Indigo);
        SolidBrush lightpurpleBrush = new SolidBrush(Color.MediumOrchid);
        SolidBrush orangeBrush = new SolidBrush(Color.OrangeRed);
        SolidBrush lightorangeBrush = new SolidBrush(Color.Coral);
        SolidBrush darkgreenBrush = new SolidBrush(Color.LimeGreen);
        SolidBrush lightgreenBrush = new SolidBrush(Color.LawnGreen);
        SolidBrush brownBrush = new SolidBrush(Color.DarkRed);
        SolidBrush lightbrownBrush = new SolidBrush(Color.SaddleBrown);
        #endregion

        SimulationBox sb = new SimulationBox();
        BufferedGraphicsContext graphicsContext;
        BufferedGraphics bufferedGraphics;
        Ball[] balls;
        double mouseX, mouseY;
        double lastMouseX, lastMouseY;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            graphicsContext = BufferedGraphicsManager.Current;
            graphicsContext.MaximumBuffer = new System.Drawing.Size(Width + 1, Height + 1);
            bufferedGraphics = graphicsContext.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            balls = CreateBalls();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            graphicsContext = BufferedGraphicsManager.Current;
            graphicsContext.MaximumBuffer = new System.Drawing.Size(Width + 1, Height + 1);

            bufferedGraphics?.Dispose();
            bufferedGraphics = graphicsContext.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mouseX = e.X;
            mouseY = e.Y;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            bufferedGraphics.Render(e.Graphics);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var graphics = bufferedGraphics.Graphics;

            DrawScene(graphics);
            DrawBalls(graphics);
            DrawCue(graphics);

            Refresh();
        }

        private void DrawScene(Graphics graphics)
        { 
            //sb.fromTheWalls();
            //sb.Collission()

            Rectangle table = new Rectangle(35, 35, 625, 325);

            graphics.FillRectangle(greenBrush, table);
            graphics.DrawRectangle(grayPen, 35, 35, 635, 335);
            #region[łuzy]
            graphics.FillEllipse(blackBrush, 36, 36, 35, 35);
            graphics.FillEllipse(blackBrush, 332, 36, 35, 35);
            graphics.FillEllipse(blackBrush, 628, 36, 35, 35);
            graphics.FillEllipse(blackBrush, 628, 335, 35, 35);
            graphics.FillEllipse(blackBrush, 332, 335, 35, 35);
            graphics.FillEllipse(blackBrush, 36, 335, 35, 35);
            #endregion
        }

        private Ball[] CreateBalls()
        {
            return new[]
            {
                new Ball { Sphere = sb.addBall(550, 190, 0, 0), Color = whiteBrush, Type = BallType.White },
                new Ball { Sphere = sb.addBall(164, 216, 0, 0), Color = yellowBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 164, 0, 0), Color = blueBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(142, 203, 0, 0), Color = redBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 216, 0, 0), Color = purpleBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 242, 0, 0), Color = orangeBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(142, 151, 0, 0), Color = darkgreenBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(186, 177, 0, 0), Color = brownBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(164, 190, 0, 0), Color = blackBrush, Type = BallType.Black },
                new Ball { Sphere = sb.addBall(208, 190, 0, 0), Color = lightyellowBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(142, 177, 0, 0), Color = lightblueBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 138, 0, 0), Color = lightredBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(186, 203, 0, 0), Color = lightpurpleBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 190, 0, 0), Color = lightorangeBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(142, 229, 0, 0), Color = lightgreenBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(164, 164, 0, 0), Color = lightbrownBrush, Type = BallType.Color },
            };
        }

        private void DrawBalls(Graphics graphics)
        {
            foreach (var ball in balls)
            {
                DrawBall(graphics, ball.Sphere, ball.Color);
            }
        }

        private void DrawBall(Graphics graphics, Sphere ball, SolidBrush color)
        {
            graphics.FillEllipse(color, (float)ball.X, (float)ball.Y, (float)ball.R, (float)ball.R);
        }

        private void DrawCue(Graphics graphics)
        {
            var whiteBall = balls.Single(ball => ball.Type == BallType.White);
            var ballX = whiteBall.X + whiteBall.R / 2;
            var ballY = whiteBall.Y + whiteBall.R / 2;
            var diffX = mouseX - ballX;
            var diffY = mouseY - ballY;
            const float MaxPower = 80.0f;

            var alpha = Math.Atan2(diffY, diffX) * 180 / Math.PI;
            var power = Math.Sqrt(diffX * diffX + diffY * diffY);
            var clippedPower = Math.Max(Math.Min(MaxPower, power), whiteBall.R / 2);

            graphics.TranslateTransform((float)ballX, (float)ballY);
            graphics.RotateTransform((float) alpha);
            graphics.TranslateTransform((float) clippedPower, 0);

            graphics.FillRectangle(brownBrush, new RectangleF(0, -5, 100, 4));

            graphics.ResetTransform();
        }


    }
}