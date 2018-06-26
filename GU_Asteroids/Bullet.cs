using System.Drawing;

namespace GU_Asteroids
{
    class Bullet:BaseObject
    {
		private int speed;
        
        public Bullet(Point pos, Point dir, Size size, int speed) : base(pos, dir, size)
        {
			this.speed = speed;
        }

        public override void Draw()
        {
            go.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X, pos.Y, size.Width, size.Height);

        }

        public override void Update()
        {
            pos.X = pos.X + speed;

			
        }
        public bool OutOfFrame()
        {
            return pos.X > go.Width;
        }        
    }
}
