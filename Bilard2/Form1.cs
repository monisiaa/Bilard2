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
        Font gameOverFont = new Font(FontFamily.GenericMonospace, 30);
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

        SimulationBoxOld sb = new SimulationBoxOld();
        BufferedGraphicsContext graphicsContext;
        BufferedGraphics bufferedGraphics;
        Ball[] balls;
        Table table;
        Scores scores;
        SimulationBox simulationBox;
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
            RestartGame();
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
            if (tableState == TableState.CueMoving)
            {
                ReleaseCue();
                Cursor.Show();
            }
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

            graphics.FillRectangle(blackBrush, new RectangleF(0, 0, Width, Height));
            table.Draw(graphics);
            DrawBalls(graphics);
            DrawCue(graphics);
            DrawTimer(graphics);
            scores.DrawScores(graphics, 700, 30);
            DrawGameOver(graphics);

            Refresh();
        }

        private void DrawGameOver(Graphics graphics)
        {
            if(tableState == TableState.GameWon)
            {
                graphics.DrawString("Wygrana", gameOverFont, redBrush, 200, 200);
            }
            else if(tableState == TableState.GameLost)
            {
                graphics.DrawString("Przegrana, czarna bila\nmusi być ostatnia", gameOverFont, redBrush, 100, 100);
            }
        }

        private Ball[] CreateBalls()
        {
            return new[]
            {
                new Ball { Sphere = sb.addBall(550, 190, 0, 0), Color = whiteBrush, Type = BallType.White, Center = new Vector(550, 190) },
                new Ball { Sphere = sb.addBall(164, 216, 0, 0), Color = yellowBrush, Type = BallType.Color, Center = new Vector(164, 216) },
                new Ball { Sphere = sb.addBall(120, 164, 0, 0), Color = blueBrush, Type = BallType.Color, Center = new Vector(120, 164) },
                new Ball { Sphere = sb.addBall(142, 203, 0, 0), Color = redBrush, Type = BallType.Color, Center = new Vector(142, 203) },
                new Ball { Sphere = sb.addBall(120, 216, 0, 0), Color = purpleBrush, Type = BallType.Color, Center = new Vector (120, 216) },
                new Ball { Sphere = sb.addBall(120, 242, 0, 0), Color = orangeBrush, Type = BallType.Color, Center = new Vector(120, 242) },
                new Ball { Sphere = sb.addBall(142, 151, 0, 0), Color = darkgreenBrush, Type = BallType.Color, Center = new Vector(142, 151) },
                new Ball { Sphere = sb.addBall(186, 177, 0, 0), Color = brownBrush, Type = BallType.Color, Center = new Vector(186, 177) },
                new Ball { Sphere = sb.addBall(164, 190, 0, 0), Color = blackBrush, Type = BallType.Black, Center = new Vector(164, 190) },
                new Ball { Sphere = sb.addBall(208, 190, 0, 0), Color = lightyellowBrush, Type = BallType.Color, Center = new Vector(208, 190) },
                new Ball { Sphere = sb.addBall(142, 177, 0, 0), Color = lightblueBrush, Type = BallType.Color, Center = new Vector(142, 177) },
                new Ball { Sphere = sb.addBall(120, 138, 0, 0), Color = lightredBrush, Type = BallType.Color, Center = new Vector(120, 138) },
                new Ball { Sphere = sb.addBall(186, 203, 0, 0), Color = lightpurpleBrush, Type = BallType.Color, Center = new Vector(186, 203) },
                new Ball { Sphere = sb.addBall(120, 190, 0, 0), Color = lightorangeBrush, Type = BallType.Color, Center = new Vector(120, 190) },
                new Ball { Sphere = sb.addBall(142, 229, 0, 0), Color = lightgreenBrush, Type = BallType.Color, Center = new Vector(142, 229) },
                new Ball { Sphere = sb.addBall(164, 164, 0, 0), Color = lightbrownBrush, Type = BallType.Color, Center = new Vector(164, 164) },
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
                    var vx = 1800 * cuePower * cuePower * -Math.Cos(cueRotationRadians);
                    var vy = 1800 * cuePower * cuePower * -Math.Sin(cueRotationRadians);

                    whiteBall.Velocity = new Vector(vx, vy);
                }

                cueDistanceFromWhiteBall -= CueReturnDistanceInTick * cuePower * cuePower;
            }

            if(tableState == TableState.BallsMoving)
            {
                simulationBox.UpdateSimulation(TimerIntervalSeconds);

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
                            ball.Velocity = new Vector(0, 0);
                            scores.AddScore(watch.Elapsed, ball.Color);

                            if(ball.Type == BallType.Black)
                            {
                                if(balls.Where(b => b.Type != BallType.White).All(b => !b.IsVisible))
                                {
                                    GameOver(true);
                                }
                                else
                                {
                                    GameOver(false);
                                }
                            }
                        }
                    }

                    // aktualizacja pozycji
                    ball.Center += ball.Velocity * TimerIntervalSeconds;

                    // tarcie
                    ball.Velocity *= 0.985;
                }

                if(balls.All(ball => ball.Velocity.Length < 5))
                {
                    tableState = TableState.None;

                    foreach(var ball in balls)
                    {
                        ball.Velocity = new Vector(0, 0);
                    }
                }
                // aktualizacja fizyki bil
            }
        }

        private void RestartWhiteBall()
        {
            var whiteBall = GetWhiteBall();
            whiteBall.Center = new Vector(550, 190);
            whiteBall.Velocity = new Vector(0, 0);
        }

        private void GameOver(bool isWin)
        {
            watch.Stop();

            if(isWin)
            {
                tableState = TableState.GameWon;
            }
            else
            {
                tableState = TableState.GameLost;
            }
        }

        private void RestartGame()
        {
            balls = CreateBalls();
            table = CreateTable();
            time = TimeSpan.Zero;
            watch = new Stopwatch();
            scores = new Scores();
            simulationBox = new SimulationBox(table, balls);
            tableState = TableState.None;
        }

        enum TableState
        {
            None,
            CueMoving,
            CueReleased,
            BallsMoving,
            GameLost,
            GameWon
        }
    }
}