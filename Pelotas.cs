using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Pelotas
{
    public partial class Pelotas : Form
    {
        static List<Pelota> balls;
        static Bitmap bmp;
        static Graphics g;
        static Random rand = new Random();
        static float deltaTime;
        static Emitter emitter;
        public SolidBrush brushOrange = new SolidBrush(Color.FromArgb(30, 255, 128, 0));
        public SolidBrush brushBrown = new SolidBrush(Color.FromArgb(255, 128, 64, 0));
        private int nBalls = 15;
        Bitmap imgbitmap = new Bitmap(Resource1.smoke);
        public Pelotas()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (PCT_CANVAS.Width == 0)
                return;

            balls       = new List<Pelota>();
            bmp         = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g           = Graphics.FromImage(bmp);
            deltaTime   = 1;
            PCT_CANVAS.Image = bmp;
            emitter = new Emitter(MousePosition.X, MousePosition.Y, 400);
            emitter.DrawFire(g);
            for (int b = 0; b < nBalls; b++)
                balls.Add(new Pelota(rand, PCT_CANVAS.Size, b, 128,emitter));
            for (int b = nBalls / 2; b < nBalls; b++)
                balls.Add(new Pelota(rand, PCT_CANVAS.Size, b, 255, emitter));
        }

        private void Pelotas_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Pelotas_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Transparent);

            emitter.x = MousePosition.X - (emitter.Size / 2);
            emitter.y = MousePosition.Y - (emitter.Size / 2);

            g.DrawImage(Resource1.stick, MousePosition.X - 280, MousePosition.Y);

            // Crear un objeto ImageAttributes
            ImageAttributes atributosImagen = new ImageAttributes();

            Pelota P;
            g.FillEllipse(brushOrange, emitter.PosX - (emitter.Size / 2), emitter.PosY - (emitter.Size / 2), emitter.Size * 2, emitter.Size * 2);
            Parallel.For(0, balls.Count, b =>//ACTUALIZAMOS EN PARALELO
            {
                balls[b].Update(rand, emitter);
                P = balls[b];
            });
            Pelota p;
            for (int b = 0; b < balls.Count / 2; b++)//PINTAMOS EN SECUENCIA
            {
                p = balls[b];
                using (Image resizedImage = resizeImage(imgbitmap, new Size((int)p.radio * 2, (int)p.radio * 2)))
                {
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = p.opacity / 255f;
                    atributosImagen.SetColorMatrix(matrix);
                    g.DrawImage(resizedImage, new Rectangle((int)p.x, (int)p.y, (int)p.radio * 2, (int)p.radio * 2), 0, 0, (int)p.radio * 2, (int)p.radio * 2, GraphicsUnit.Pixel, atributosImagen);
                    p.opacity -= 9;
                }
                //g.FillEllipse(new SolidBrush(p.c), p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);
            }
            emitter.DrawFire(g);
            for (int b = balls.Count / 2; b < balls.Count; b++)//PINTAMOS EN SECUENCIA
            {
                p = balls[b];
                using (Image resizedImage = resizeImage(imgbitmap, new Size((int)p.radio * 2, (int)p.radio * 2)))
                {
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = p.opacity / 255f;
                    atributosImagen.SetColorMatrix(matrix);
                    g.DrawImage(resizedImage, new Rectangle((int)p.x, (int)p.y, (int)p.radio * 2, (int)p.radio * 2), 0, 0, (int)p.radio * 2, (int)p.radio * 2, GraphicsUnit.Pixel, atributosImagen);
                    p.opacity -= 7;
                }
                //g.FillEllipse(new SolidBrush(p.c), p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);
            }
            PCT_CANVAS.Invalidate();
            deltaTime += .1f;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
