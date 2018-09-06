using System;
using System.Collections.Generic;
using System.Drawing;

namespace Bilard2
{
    public class Scores
    {
        List<TimeSpan> Times = new List<TimeSpan>();
        List<Brush> Colors = new List<Brush>();
        Font scoreFont = new Font(FontFamily.GenericMonospace, 10);
        
        public void AddScore(TimeSpan time, Brush color)
        {
            Colors.Add(color);
            Times.Add(time);
        }

        public void DrawScores(Graphics graphics, int x, int y)
        {
            for(int i = 0; i < Times.Count; i++)
            {
                graphics.FillEllipse(Colors[i], x, y, 10, 10);
                graphics.DrawString(Times[i].ToString("c"), scoreFont, Colors[i], x + 20, y);
                y += 30;
            }
        }
    }
}
