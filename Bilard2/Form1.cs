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
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
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

            int x_w = 550, y_w = 190;
            Ball white = sb.addBall(x_w, y_w, 0, 0);
            Graphics WhiteBall = CreateGraphics();
            WhiteBall.FillEllipse(whiteBrush, white.X , white.Y, white.R, white.R);
        }
    }
}
