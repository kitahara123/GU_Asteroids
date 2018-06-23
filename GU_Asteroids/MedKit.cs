using System.Drawing;

namespace GU_Asteroids
{
	class MedKit : BaseObject
	{
		public int Power { get; set; }
		public MedKit(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
			Power = 10;
		}

		public override void Draw()
		{
			go.Buffer.Graphics.DrawLine(Pens.Green, pos.X - size.Width, pos.Y, pos.X + size.Width, pos.Y);
			go.Buffer.Graphics.DrawLine(Pens.Green, pos.X, pos.Y - size.Height, pos.X, pos.Y + size.Height);
		}

		public override void Update()
		{
		}
	}
}
