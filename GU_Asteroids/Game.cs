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
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

		static Game()
		{
		}

		public static void Load ()
		{
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(40, 10));
            _asteroids = new Asteroid[3];
            var rnd = new Random();

            for (int i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }

            for (int i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }




            /*			_objs = new BaseObject[45];
                        for (int i = 0; i < _objs.Length / 3; i++)
                        {
                            _objs[i] = new Asteroid(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));
                        }

                        for (int i = _objs.Length / 3; i < _objs.Length / 1.5 ; i++)
                        {
                            _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
                        }

                        for (int i = (int)(_objs.Length / 1.5); i < _objs.Length - 1; i++)
                        {
                            _objs[i] = new WierdFaces(new Point(600, 200), new Point(i-29, i-29), i-20);
                        }
                        _objs[44] = new SpaceBoi(new Point(350, 200), new Point(350, 250), new Point(400, 200), new Point(5,5)); */

        }

		public static void Init(Form form)
		{
			Timer t = new Timer { Interval = 100 };
			t.Start();
			t.Tick += Timer_Tick;

			Graphics g;
			_context = BufferedGraphicsManager.Current;
			g = form.CreateGraphics();

			Width = form.Width;
			Height = form.Height;

			buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
		}

		public static void Draw()
		{

			buffer.Graphics.Clear(Color.Black);

			foreach (BaseObject bo in _objs)
			{
				bo.Draw();
			}
            foreach (Asteroid a in _asteroids)
            {
                a.Draw();
            }
            _bullet.Draw();


			buffer.Render();
		}

		private static void Update()
		{
			foreach (BaseObject bo in _objs)
			{
				bo.Update();
			}
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet)) { System.Media.SystemSounds.Hand.Play(); }
            }
		}

		private static void Timer_Tick(object sender, EventArgs e)
		{
			Draw();
			Update();
		}

	}
}
