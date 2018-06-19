using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GU_Asteroids
{
    class Bullet:BaseObject
    {

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            go.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X, pos.Y, size.Width, size.Height);

        }

        public override void Update()
        {
            pos.X = pos.X + 3;
        }
    }
}
