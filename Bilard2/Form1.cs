using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Bilard2
{
    public partial class Form1 : Form
    {
        #region[brush & pen]
        Pen grayPen = new Pen(Color.Gray, TableEdgeWidth * 2);
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
        Font timerFont = new Font(FontFamily.GenericMonospace, 15);
        #endregion

        const int TimerIntervalMiliseconds = 10;
        const double TimerIntervalSeconds = TimerIntervalMiliseconds / 1000.0f;
        const float MaxCueDistance = 80.0f;
        const int CueReturnTimeTicks = 10;
        const float CueReturnDistanceInTick = MaxCueDistance / CueReturnTimeTicks;
        const int TableX = 35;
        const int TableY = 35;
        const int TableW = 635;
        const int TableH = 335;
        const int TableEdgeWidth = 10;

        SimulationBox sb = new SimulationBox();
        BufferedGraphicsContext graphicsContext;
        BufferedGraphics bufferedGraphics;
        Ball[] balls;
        Table table;
        Scores scores;
        double mouseX, mouseY;
        double lastMouseX, lastMouseY;
        double cueDistanceFromWhiteBall;
        double cuePower;
        double cueRotation;
        double cueX;
        double cueY;

        bool enableCue;
        TableState tableState;
        TimeSpan time;
        Stopwatch watch;
               
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            graphicsContext = BufferedGraphicsManager.Current;
            graphicsContext.MaximumBuffer = new System.Drawing.Size(Width + 1, Height + 1);
            bufferedGraphics = graphicsContext.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            balls = CreateBalls();
            table = CreateTable();
            time = TimeSpan.Zero;
            watch = new Stopwatch();
            scores = new Scores();
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
            timer1.Interval = 10;
            watch.Start();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mouseX = e.X;
            mouseY = e.Y;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var whiteBall = GetWhiteBall();
            var x = whiteBall.X + whiteBall.R - e.X;
            var y = whiteBall.Y + whiteBall.R - e.Y;
            var r = whiteBall.R;

            if (Math.Sqrt(x * x + y * y) < r && tableState == TableState.None)
            {
                tableState = TableState.CueMoving;
                Cursor.Hide();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            ReleaseCue();
            Cursor.Show();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            bufferedGraphics.Render(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var graphics = bufferedGraphics.Graphics;

            time = time.Add(TimeSpan.FromMilliseconds(TimerIntervalMiliseconds));
            UpdateTable();

            DrawScene(graphics);
            DrawBalls(graphics);
            DrawCue(graphics);
            DrawTimer(graphics);
            scores.DrawScores(graphics, 700, 50);

            Refresh();
        }

        private void DrawScene(Graphics graphics)
        {
            /*var tableRect = new Rectangle(TableX, TableY, TableW, TableH);

            
            graphics.FillRectangle(greenBrush, tableRect);
            graphics.DrawRectangle(grayPen, tableRect);
            #region[łuzy]
            graphics.FillEllipse(blackBrush, 36, 36, 35, 35);
            graphics.FillEllipse(blackBrush, 332, 36, 35, 35);
            graphics.FillEllipse(blackBrush, 628, 36, 35, 35);
            graphics.FillEllipse(blackBrush, 628, 335, 35, 35);
            graphics.FillEllipse(blackBrush, 332, 335, 35, 35);
            graphics.FillEllipse(blackBrush, 36, 335, 35, 35);
            #endregion*/
            graphics.FillRectangle(blackBrush, new RectangleF(0, 0, Width, Height));
            table.Draw(graphics);
        }

        private Ball[] CreateBalls()
        {
            return new[]
            {
                new Ball { Sphere = sb.addBall(550, 190, 0, 0), Color = whiteBrush, Type = BallType.White },
                new Ball { Sphere = sb.addBall(164, 216, 0, 0), Color = yellowBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(120, 164, 0, 0), Color = blueBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(142, 203, 0, 0), Color = redBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(120, 216, 0, 0), Color = purpleBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 242, 0, 0), Color = orangeBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(142, 151, 0, 0), Color = darkgreenBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(186, 177, 0, 0), Color = brownBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(164, 190, 0, 0), Color = blackBrush, Type = BallType.Black },
                new Ball { Sphere = sb.addBall(208, 190, 0, 0), Color = lightyellowBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(142, 177, 0, 0), Color = lightblueBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 138, 0, 0), Color = lightredBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(186, 203, 0, 0), Color = lightpurpleBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(120, 190, 0, 0), Color = lightorangeBrush, Type = BallType.Color },
                //new Ball { Sphere = sb.addBall(142, 229, 0, 0), Color = lightgreenBrush, Type = BallType.Color },
                new Ball { Sphere = sb.addBall(164, 164, 0, 0), Color = lightbrownBrush, Type = BallType.Color },
            };
        }

        private Table CreateTable()
        {
            Table table = new Table(TableX, TableY, TableW, TableH, TableEdgeWidth, greenBrush, grayPen);
            table.AddHole(36, 36, 17);
            table.AddHole(332, 36, 17);
            table.AddHole(628, 36, 17);
            table.AddHole(628, 335, 17);
            table.AddHole(332, 335, 17);
            table.AddHole(36, 335, 17);
            return table;
        }

        private void DrawBalls(Graphics graphics)
        {
            foreach (var ball in balls)
            {
                ball.Draw(graphics);
            }
        }

        private void DrawTimer(Graphics graphics)
        {
            time = watch.Elapsed;
            graphics.DrawString(time.ToString("c"), timerFont, whiteBrush, 300, 400);
        }

        private Ball GetWhiteBall()
        {
            return balls.Single(ball => ball.Type == BallType.White);
        }

        private void DrawCue(Graphics graphics)
        {
            if (tableState != TableState.CueMoving && tableState != TableState.CueReleased)
            {
                return;
            }

            graphics.TranslateTransform((float)cueX, (float) cueY);
            graphics.RotateTransform((float) cueRotation);
            graphics.TranslateTransform((float) cueDistanceFromWhiteBall, 0);

            graphics.FillRectangle(brownBrush, new RectangleF(0, -5, 100, 4));

            graphics.ResetTransform();
        }

        private void ReleaseCue()
        {
            cuePower = cueDistanceFromWhiteBall / MaxCueDistance;
            tableState = TableState.CueReleased;
        }

        private void UpdateTable()
        {
            if(tableState == TableState.None)
            {
                return;
            }

            if(tableState == TableState.CueMoving)
            {
                var whiteBall = GetWhiteBall();
                cueX = whiteBall.X + whiteBall.R;
                cueY = whiteBall.Y + whiteBall.R;
                var diffX = mouseX - cueX;
                var diffY = mouseY - cueY;

                cueRotation = Math.Atan2(diffY, diffX) * 180 / Math.PI;
                var distance = Math.Sqrt(diffX * diffX + diffY * diffY);

                cueDistanceFromWhiteBall = Math.Max(Math.Min(MaxCueDistance, distance), whiteBall.R);
            }

            if(tableState == TableState.CueReleased)
            {
                var whiteBall = GetWhiteBall();

                if(cueDistanceFromWhiteBall <= whiteBall.R)
                {
                    var cueRotationRadians = cueRotation * Math.PI / 180.0f;

                    tableState = TableState.BallsMoving;
                    whiteBall.Sphere.VX = 20 * cuePower * cuePower * -Math.Cos(cueRotationRadians);
                    whiteBall.Sphere.VY = 20 * cuePower * cuePower * -Math.Sin(cueRotationRadians);
                }

                cueDistanceFromWhiteBall -= CueReturnDistanceInTick * cuePower * cuePower;
            }

            if(tableState == TableState.BallsMoving)
            {
                sb.fromTheWalls(TableX + TableEdgeWidth, TableY + TableEdgeWidth, TableW - 2 * TableEdgeWidth, TableH - 2 * TableEdgeWidth);
                sb.Collission();

                foreach(var ball in balls)
                {
                    if(!ball.IsVisible)
                    {
                        continue;
                    }

                    if(table.IsBallInsideHole(ball))
                    {
                        if (ball.Type == BallType.White)
                        {
                            RestartWhiteBall();
                        }
                        else
                        {
                            ball.IsVisible = false;
                            sb.removeBall(ball.Sphere);
                            ball.Sphere.VX = 0;
                            ball.Sphere.VY = 0;
                            scores.AddScore(watch.Elapsed, ball.Color);
                        }
                    }

                    // aktualizacja pozycji
                    ball.Sphere.X += ball.Sphere.VX * TimerIntervalSeconds;
                    ball.Sphere.Y += ball.Sphere.VY * TimerIntervalSeconds;

                    // tarcie
                    ball.Sphere.VX *= 0.98;
                    ball.Sphere.VY *= 0.98;
                }

                if(balls.All(ball => Math.Abs(ball.Sphere.VX) < 0.15 && Math.Abs(ball.Sphere.VY) < 0.15))
                {
                    tableState = TableState.None;

                    foreach(var ball in balls)
                    {
                        ball.Sphere.VX = 0;
                        ball.Sphere.VY = 0;
                    }
                }
                // aktualizacja fizyki bil
            }
        }

        private void RestartWhiteBall()
        {
            var whiteBall = GetWhiteBall();
            whiteBall.Sphere.X = 550;
            whiteBall.Sphere.Y = 190;
            whiteBall.Sphere.VX = 0;
            whiteBall.Sphere.VY = 0;
        }

        enum TableState
        {
            None,
            CueMoving,
            CueReleased,
            BallsMoving
        }
    }
}