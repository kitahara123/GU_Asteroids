using System.Drawing;

namespace GU_Asteroids
{
	class Star : BaseObject
	{
		public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
        }

		public override void Draw()
		{
            go.Buffer.Graphics.DrawLine(Pens.White, pos.X, pos.Y, pos.X + size.Width,
			pos.Y + size.Height);
            go.Buffer.Graphics.DrawLine(Pens.White, pos.X + size.Width, pos.Y, pos.X,
			pos.Y + size.Height);
		}

		public override void Update()
		{
			pos.X = pos.X + dir.X;
			if (pos.X < 0) pos.X = go.Width + size.Width;
		}



	}
}
