using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pelotas
{
    public partial class Balls : Form
    {
        List<Ball> balls;
        Bitmap bmp;
        Graphics g;
        static Random rand = new Random();
        public Balls()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (PCT_CANVAS.Width == 0)
                return;

            balls   = new List<Ball>();
            bmp     = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g       = Graphics.FromImage(bmp);

            PCT_CANVAS.Image = bmp;
            for (int i = 0; i < 30; i++)
                balls.Add(new Ball(rand, PCT_CANVAS.Size, i));
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Black);
            Ball b;
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Update(balls);
                b = balls[i];
                g.FillEllipse(new SolidBrush(balls[i].c), b.x - b.sizeR / 2, b.y - b.sizeR / 2, b.sizeR, b.sizeR);
                balls[i].changed = false;
            }

            PCT_CANVAS.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }
    }
}
