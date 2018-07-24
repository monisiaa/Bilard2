using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void bStart_Click(object sender, EventArgs e)
        {

            #region[bile]
            int x_w = 550, y_w = 190, vx_w = 0, vy_w = 0;
            Ball white = sb.addBall(x_w, y_w, vx_w, vy_w);
           // sb.balls.Add(white);
            Graphics WhiteBall = CreateGraphics();
            WhiteBall.FillEllipse(whiteBrush, (float)white.X, (float)white.Y, (float)white.R, (float)white.R);

            int x_1 = 164, y_1 = 216, vx_1 = 0, vy_1 = 0;
            Ball yellow = sb.addBall(x_1, y_1, vx_1, vy_1);
            sb.balls.Add(yellow);
            Graphics YellowBall = CreateGraphics();
            YellowBall.FillEllipse(yellowBrush, (float)yellow.X, (float)yellow.Y, (float)yellow.R, (float)yellow.R);

            int x_2 = 120, y_2 = 164, vx_2 = 0, vy_2 = 0;
            Ball blue = sb.addBall(x_2, y_2, vx_2, vy_2);
            sb.balls.Add(blue);
            Graphics BlueBall = CreateGraphics();
            BlueBall.FillEllipse(blueBrush, (float)blue.X, (float)blue.Y, (float)blue.R, (float)blue.R);

            int x_3 = 142, y_3 = 203, vx_3 = 0, vy_3 = 0;
            Ball red = sb.addBall(x_3, y_3, vx_3, vy_3);
            sb.balls.Add(red);
            Graphics RedBall = CreateGraphics();
            RedBall.FillEllipse(redBrush, (float)red.X, (float)red.Y, (float)red.R, (float)red.R);

            int x_4 = 120, y_4 = 216, vx_4 = 0, vy_4 = 0;
            Ball purple = sb.addBall(x_4, y_4, vx_4, vy_4);
            sb.balls.Add(purple);
            Graphics PurpleBall = CreateGraphics();
            PurpleBall.FillEllipse(purpleBrush, (float)purple.X, (float)purple.Y, (float)purple.R, (float)purple.R);

            int x_5 = 120, y_5 = 242, vx_5 = 0, vy_5 = 0;
            Ball orange = sb.addBall(x_5, y_5, vx_5, vy_5);
            sb.balls.Add(orange);
            Graphics OrangeBall = CreateGraphics();
            OrangeBall.FillEllipse(orangeBrush, (float)orange.X, (float)orange.Y, (float)orange.R, (float)orange.R);

            int x_6 = 142, y_6 = 151, vx_6 = 0, vy_6 = 0;
            Ball darkgreen = sb.addBall(x_6, y_6, vx_6, vy_6);
            sb.balls.Add(darkgreen);
            Graphics DarkGreenBall = CreateGraphics();
            DarkGreenBall.FillEllipse(darkgreenBrush, (float)darkgreen.X, (float)darkgreen.Y, (float)darkgreen.R, (float)darkgreen.R);

            int x_7 = 186, y_7 = 177, vx_7 = 0, vy_7 = 0;
            Ball brown = sb.addBall(x_7, y_7, vx_7, vy_7);
            sb.balls.Add(brown);
            Graphics BrownBall = CreateGraphics();
            BrownBall.FillEllipse(brownBrush, (float)brown.X, (float)brown.Y, (float)brown.R, (float)brown.R);

            int x_8 = 164, y_8 = 190, vx_8 = 0, vy_8 = 0;
            Ball black = sb.addBall(x_8, y_8, vx_8, vy_8);
            sb.balls.Add(black);
            Graphics BlackBall = CreateGraphics();
            BlackBall.FillEllipse(blackBrush, (float)black.X, (float)black.Y, (float)black.R, (float)black.R);

            int x_9 = 208, y_9 = 190, vx_9 = 0, vy_9 = 0;
            Ball lightyellow = sb.addBall(x_9, y_9, vx_9, vy_9);
            sb.balls.Add(lightyellow);
            Graphics LightYellowBall = CreateGraphics();
            LightYellowBall.FillEllipse(lightyellowBrush, (float)lightyellow.X, (float)lightyellow.Y, (float)lightyellow.R, (float)lightyellow.R);

            int x_10 = 142, y_10 = 177, vx_10 = 0, vy_10 = 0;
            Ball lightblue = sb.addBall(x_10, y_10, vx_10, vy_10);
            sb.balls.Add(lightblue);
            Graphics LightBlueBall = CreateGraphics();
            LightBlueBall.FillEllipse(lightblueBrush, (float)lightblue.X, (float)lightblue.Y, (float)lightblue.R, (float)lightblue.R);

            int x_11 = 120, y_11 = 138, vx_11 = 0, vy_11 = 0;
            Ball lightred = sb.addBall(x_11, y_11, vx_11, vy_11);
            sb.balls.Add(lightred);
            Graphics LightRedBall = CreateGraphics();
            LightRedBall.FillEllipse(lightredBrush, (float)lightred.X, (float)lightred.Y, (float)lightred.R, (float)lightred.R);

            int x_12 = 186, y_12 = 203, vx_12 = 0, vy_12 = 0;
            Ball lightpurple = sb.addBall(x_12, y_12, vx_12, vy_12);
            sb.balls.Add(lightpurple);
            Graphics LightPurpleBall = CreateGraphics();
            LightPurpleBall.FillEllipse(lightpurpleBrush, (float)lightpurple.X, (float)lightpurple.Y, (float)lightpurple.R, (float)lightpurple.R);

            int x_13 = 120, y_13 = 190, vx_13 = 0, vy_13 = 0;
            Ball lightorange = sb.addBall(x_13, y_13, vx_13, vy_13);
            sb.balls.Add(lightorange);
            Graphics LightOrangeBall = CreateGraphics();
            LightOrangeBall.FillEllipse(lightorangeBrush, (float)lightorange.X, (float)lightorange.Y, (float)lightorange.R, (float)lightorange.R);

            int x_14 = 142, y_14 = 229, vx_14 = 0, vy_14 = 0;
            Ball lightgreen = sb.addBall(x_14, y_14, vx_14, vy_14);
            sb.balls.Add(lightgreen);
            Graphics LightGreenBall = CreateGraphics();
            LightGreenBall.FillEllipse(lightgreenBrush, (float)lightgreen.X, (float)lightgreen.Y, (float)lightgreen.R, (float)lightgreen.R);

            int x_15 = 164, y_15 = 164, vx_15 = 0, vy_15 = 0;
            Ball lightbrown = sb.addBall(x_15, y_15, vx_15, vy_15);
            sb.balls.Add(lightbrown);
            Graphics LightBrownBall = CreateGraphics();
            LightBrownBall.FillEllipse(lightbrownBrush, (float)lightbrown.X, (float)lightbrown.Y, (float)lightbrown.R, (float)lightbrown.R);
            #endregion

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();

            sb.fromTheWalls();
            sb.Collission();

            Rectangle table = new Rectangle(35, 35, 625, 325);

            Graphics t = CreateGraphics();
            Graphics r = CreateGraphics();
            t.FillRectangle(greenBrush, table);
            r.DrawRectangle(grayPen, 35, 35, 635, 335);
            #region[łuzy]
            Graphics l1 = CreateGraphics();
            l1.FillEllipse(blackBrush, 36, 36, 35, 35);
            Graphics l2 = CreateGraphics();
            l2.FillEllipse(blackBrush, 332, 36, 35, 35);
            Graphics l3 = CreateGraphics();
            l3.FillEllipse(blackBrush, 628, 36, 35, 35);
            Graphics l4 = CreateGraphics();
            l4.FillEllipse(blackBrush, 628, 335, 35, 35);
            Graphics l5 = CreateGraphics();
            l5.FillEllipse(blackBrush, 332, 335, 35, 35);
            Graphics l6 = CreateGraphics();
            l6.FillEllipse(blackBrush, 36, 335, 35, 35);
            #endregion
        }
    }
}