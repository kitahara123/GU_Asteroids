using System;
using System.Drawing;

namespace GU_Asteroids
{
    class Asteroid : BaseObject, ICloneable
    {
        public int Power { get; set; }

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }


        public override void Draw()
        {
            go.Buffer.Graphics.FillEllipse(Brushes.White, pos.X, pos.Y, size.Width, size.Height);
        }

        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(pos.X, pos.Y), new Point(dir.X, dir.Y), new Size(size.Width, size.Height));
            asteroid.Power = Power;
            return asteroid;
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;

            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X > go.Width) dir.X = -dir.X;

            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > go.Height) dir.Y = -dir.Y;
        }

        public void Destroy()
        {
            go.Buffer.Graphics.FillEllipse(Brushes.Red, pos.X, pos.Y, size.Width - 5, size.Height - 5);
            go.Buffer.Graphics.FillEllipse(Brushes.Yellow, pos.X, pos.Y, size.Width, size.Height);
            go.Buffer.Graphics.FillEllipse(Brushes.Red, pos.X, pos.Y, size.Width +10, size.Height +10);
            go.Buffer.Render();
        }
    }
}
