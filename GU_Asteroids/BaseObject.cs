using System.Drawing;

namespace GU_Asteroids
{
	public delegate void Message();

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

        public virtual Rectangle Rect => new Rectangle(pos, size);

        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

        public abstract void Draw();
        public abstract void Update();

	}
}
