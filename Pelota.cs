using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Pelotas
{
    public class Pelota
    {
        int index;
        Size space;
        public Color c;
        // Variables de posición
        public float x;
        public float y;

        // Variables de velocidad
        private float vx;
        private float vy;

        // Variable de radio
        public float radio;

        public int initialOpacity;
        public int opacity;

        // Constructor
        public Pelota(Random rand, Size size, int index, int opacity, Emitter emitter)
        {
            this.radio  = rand.Next(70, 100);
            this.x      = -100;
            this.y      = -100;         
            c           = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            this.vy = rand.Next(15, (int)radio - 50);
            this.opacity = opacity;
            this.initialOpacity = opacity;

            this.index = index;
            space = size;
        }

        public int PosX
        {
            set { this.x = value; }
        }

        public int PosY
        {
            set { this.y = value; }
        }

        // Método para actualizar la posición de la pelota en función de su velocidad
        public void Update(Random rand, Emitter emitter)
        {
            this.y -= this.vy;
            if (y <= 0 - radio)
            {
                x = rand.Next(emitter.PosX - 50, emitter.PosX + emitter.Size - ((int)radio * 2 )+ 50);
                y = emitter.PosY + (emitter.Size / 2);
                opacity = initialOpacity;
            }
        }
    }

}
