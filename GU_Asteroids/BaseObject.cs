using System.Drawing;

namespace GU_Asteroids
{
	abstract class BaseObject: ICollision
	{
		protected Point pos;
		protected Point dir;
		protected Size size;
        protected Game go;

		protected BaseObject (Point pos, Point dir, Size size)
		{
            go = Game.getGameObject;
			this.pos = pos;
			this.dir = dir;
			this.size = size;
		}

        public Rectangle Rect => new Rectangle(pos, size);

        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

        public abstract void Draw();

        //	Game.buffer.Graphics.DrawEllipse(Pens.White, pos.X, pos.Y, size.Width, size.Height);


        public abstract void Update();

	}
}
