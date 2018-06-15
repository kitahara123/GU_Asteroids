using System;
using System.Drawing;
using System.Windows.Forms;

namespace GU_Asteroids
{
	class Game
	{
		private static BufferedGraphicsContext _context;
		public static BufferedGraphics buffer;

		public static int Width { get; set; }
		public static int Height { get; set; }

		private static BaseObject[] _objs;

		static Game()
		{
		}

		private static void Load ()
		{
			_objs = new BaseObject[45];
			for (int i = 0; i < _objs.Length / 3; i++)
			{
				_objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));
			}

			for (int i = _objs.Length / 3; i < _objs.Length / 1.5 ; i++)
			{
				_objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
			}

			for (int i = (int)(_objs.Length / 1.5); i < _objs.Length - 1; i++)
			{
				_objs[i] = new WierdFaces(new Point(600, 200), new Point(i-29, i-29), i-20);
			}
			_objs[44] = new SpaceBoi(new Point(350, 200), new Point(350, 250), new Point(400, 200), new Point(5,5));

		}

		public static void Init(Form form)
		{
			Timer t = new Timer { Interval = 100 };
			t.Start();
			t.Tick += Timer_Tick;
			Load();

			Graphics g;
			_context = BufferedGraphicsManager.Current;
			g = form.CreateGraphics();

			Width = form.Width;
			Height = form.Height;

			buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
		}

		public static void Draw()
		{
//			buffer.Graphics.Clear(Color.Black);
//			buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
//			buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
//			buffer.Render();

			buffer.Graphics.Clear(Color.Black);

			foreach (BaseObject bo in _objs)
			{
				bo.Draw();
			}
			buffer.Render();
		}

		private static void Update()
		{
			foreach (BaseObject bo in _objs)
			{
				bo.Update();
			}
		}

		private static void Timer_Tick(object sender, EventArgs e)
		{
			Draw();
			Update();
		}

	}
}
