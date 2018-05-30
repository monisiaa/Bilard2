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

namespace Bilard2
{
    public partial class Form1 : Form
    {
        int x, y, width, height;
        
        SimulationBox sb = new SimulationBox();

        public Form1()
        {
            InitializeComponent();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            Pen grayPen = new Pen(Color.Gray, 20);
            Pen whitePen = new Pen(Color.White, 10);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush purpleBrush = new SolidBrush(Color.Purple);
            SolidBrush orangeBrush = new SolidBrush(Color.Orange);
            SolidBrush darkgreenBrush = new SolidBrush(Color.DarkGreen);
            SolidBrush brownBrush = new SolidBrush(Color.Brown);
            Rectangle table = new Rectangle(35, 35, 625, 325);

            Graphics t = CreateGraphics();
            Graphics r = CreateGraphics();
            t.FillRectangle(greenBrush, table);
            r.DrawRectangle(grayPen, 35, 35, 635, 335);

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

            int x_w = 550, y_w = 190, vx_w=0, vy_w=0;
            Ball white = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics WhiteBall = CreateGraphics();
            WhiteBall.FillEllipse(whiteBrush, white.X, white.Y, white.R, white.R);

            int x_1, y_1, vx_1, vy_1;
            Ball yellow = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics YellowBall = CreateGraphics();
            WhiteBall.FillEllipse(yellowBrush, yellow.X, yellow.Y, yellow.R, yellow.R);

            int x_2, y_2, vx_2, vy_2;
            Ball blue = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics BlueBall = CreateGraphics();
            WhiteBall.FillEllipse(blueBrush, blue.X, blue.Y, blue.R, blue.R);

            int x_3, y_3, vx_3, vy_3;
            Ball red = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics RedBall = CreateGraphics();
            WhiteBall.FillEllipse(redBrush, red.X, red.Y, red.R, red.R);

            int x_4, y_4, vx_4, vy_4;
            Ball purple = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics PurpleBall = CreateGraphics();
            WhiteBall.FillEllipse(purpleBrush, purple.X, purple.Y, purple.R, purple.R);

            int x_5, y_5, vx_5, vy_5;
            Ball orange = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics OrangeBall = CreateGraphics();
            WhiteBall.FillEllipse(orangeBrush, orange.X, orange.Y, orange.R, orange.R);

            int x_6, y_6, vx_6, vy_6;
            Ball darkgreen = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics DarkGreenBall = CreateGraphics();
            WhiteBall.FillEllipse(darkgreenBrush, darkgreen.X, darkgreen.Y, darkgreen.R, darkgreen.R);

            int x_7, y_7, vx_7, vy_7;
            Ball brown = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics BrownBall = CreateGraphics();
            WhiteBall.FillEllipse(brownBrush, brown.X, brown.Y, brown.R, brown.R);

            int x_8, y_8, vx_8, vy_8;
            Ball black = sb.addBall(x_w, y_w, vx_w, vy_w);
            Graphics BlackBall = CreateGraphics();
            WhiteBall.FillEllipse(blackBrush, black.X, black.Y, black.R, black.R);
        }
    }
}
