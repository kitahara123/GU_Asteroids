using System.Drawing;

namespace GU_Asteroids
{
	class Ship : BaseObject
	{
		private const int MAX_ENERGY = 100;
		private int _energy = MAX_ENERGY;
		Image img;

		Point upR;
		Point upL;
		Point downR;

		int speed;
		public int Energy { get => _energy; set => _energy = value; }

		public void EnergyLow(int n)
		{
			Energy -= n;
		}
		public void Heal(int n)
		{
			if (Energy + n > MAX_ENERGY)
				Energy = MAX_ENERGY;
			else
				Energy += n;
		}

		// Точка откалибрована примерно серединой модельки
		public override Rectangle Rect => new Rectangle(downR.X - 60, downR.Y - 30, 40, 20);

		public Point GunPoint => new Point(downR.X -24, downR.Y -8);

		

		public Ship(Point pos1, Point pos2, Point pos3, int speed) : base(new Point(), new Point(), new Size()) // я не понимаю как тут правильно это сделать
		{
			upR = pos1;
			upL = pos2;
			downR = pos3;
			this.speed = speed;
			img = Image.FromFile("spaceShip.jpg");
		}

		

		public override void Draw()
		{
			Point[] points = new Point[3] { upR, upL, downR };
			go.Buffer.Graphics.DrawImage(img, points);
		}

		public override void Update()
		{
		}

		public void Up()
		{
			if (upR.Y > 0) {
				upR.Y = upR.Y - speed;
				upL.Y = upL.Y - speed;
				downR.Y = downR.Y - speed;
			}

		}
		public void Down()
		{
			if (downR.Y < go.Height - 30)
			{
				upR.Y = upR.Y + speed;
				upL.Y = upL.Y + speed;
				downR.Y = downR.Y + speed;
			}

		}

		public void Die()
		{
			MessageDie?.Invoke();
		}
		public static event Message MessageDie;
	}
}
