using System.Drawing;

namespace GU_Asteroids
{
	class BaseObject
	{
		protected Point pos;
		protected Point dir;
		protected Size size;

		public BaseObject (Point pos, Point dir, Size size)
		{
			this.pos = pos;
			this.dir = dir;
			this.size = size;
		}

		public virtual void Draw()
		{
			Game.buffer.Graphics.DrawEllipse(Pens.White, pos.X, pos.Y, size.Width, size.Height);
		}

		public virtual void Update()
		{
			pos.X = pos.X + dir.X;
			pos.Y = pos.Y + dir.Y;

			if (pos.X < 0) dir.X = -dir.X;
			if (pos.X > Game.Width) dir.X = -dir.X;

			if (pos.Y < 0) dir.Y = -dir.Y;
			if (pos.Y > Game.Height) dir.Y = -dir.Y;

		}
	}
}
