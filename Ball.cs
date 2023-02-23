using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pelotas
{
    public class Ball
    {
        static Size space;
        public float sizeR;
        public int x, y, speedX,speedY, index;
        public Color c;
        public bool changed;

        public Ball(Random rand, Size size, int index)
        {
            x           = rand.Next(0, size.Width);
            y           = rand.Next(0, size.Height);
            speedX      = rand.Next(-10, 10);
            speedY      = rand.Next(-10, 10);
            sizeR       = rand.Next(45,60);
            space       = size;
            this.index  = index;
            changed     = false;


            c       = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        public Boolean Collide(Ball ball)
        {
            return (Distance2(ball) < (sizeR / 2) + (ball.sizeR / 2));
        }

        public float Distance2(Ball ball)
        {
            return (float)Math.Sqrt((x - ball.x)*(x - ball.x) + (y - ball.y) * (y - ball.y));
        }

        public void Update(List<Ball> balls)
        {
            if (!changed)
            for (int b = index+1; b < balls.Count; b++)
            {
                if (Collide(balls[b]))
                {
                    speedX *= -1;
                    speedY *= -1;

                    balls[b].speedX *= 1;
                    balls[b].speedY *= 1;
                    balls[b].changed = true;
                }
            }

            if ((x - (sizeR / 2)) <= 0 || (x + (sizeR / 2)) >= space.Width)
            {
                changed = true;
                speedX *= -1;
            }
            if ((y - (sizeR / 2)) <= 0 || (y + (sizeR / 2)) >= space.Height)
            {
                speedY *= -1;
                changed = true;
            }
            x += speedX;
            y += speedY;
        }

    }
}
