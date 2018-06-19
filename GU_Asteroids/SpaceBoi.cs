using System.Drawing;


namespace GU_Asteroids
{
	class SpaceBoi : BaseObject
	{
		Point pos1;
		Point pos2;
		Point pos3;
        Image img;
		public SpaceBoi(Point pos1, Point pos2, Point pos3, Point dir) : base(new Point(), dir, new Size()) // я не понимаю как тут правильно это сделать
		{
            this.pos1 = pos1;
			this.pos2 = pos2;
			this.pos3 = pos3;
            img = Image.FromFile("VmZ6l7v.png");
            img.RotateFlip(RotateFlipType.Rotate90FlipX);
		}
       
		public override void Draw()
		{
			
			Point[] points = new Point[3] { pos1, pos2, pos3 };

			go.Buffer.Graphics.DrawImage(img, points);

		}

		public override void Update()
		{
			pos1.X = pos1.X - dir.X;
			pos2.X = pos2.X - dir.X;
			pos3.X = pos3.X - dir.X;

			pos1.Y = pos1.Y - dir.Y;
			pos2.Y = pos2.Y - dir.Y;
			pos3.Y = pos3.Y - dir.Y;

			if (pos1.X < 0 || pos2.X < 0 || pos3.X < 0)
			{
				dir.X = -dir.X;
			}
			if (pos1.X > go.Width || pos2.X > go.Width || pos3.X > go.Width)
			{
				dir.X = -dir.X;
			}

			if (pos1.Y < 0 || pos2.Y < 0 || pos3.Y < 0)
			{
				dir.Y = -dir.Y;
			}
			if (pos1.Y > go.Height || pos2.Y > go.Height || pos3.Y > go.Height)
			{
				dir.Y = -dir.Y;
			}
			
			
		}
	}
}
