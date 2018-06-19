using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GU_Asteroids
{
	class Game
	{
        private static Game game = null;
        private const int ASTEROIDS_INITIAL_AMOUNT = 30;
        private const int STARS_INITIAL_AMOUNT = 50;
        private const int ASTEROIDS_X_SPAWNPOINT = -100;
        private const int STARS_X_SPAWNPOINT = 1000;

        private const int MAX_WIDTH = 1000;
        private const int MAX_HEiGHT = 1000;

        private BufferedGraphicsContext _context;

        private int width;
        private int height;

        public int Width
        {
            get { return width; }
            set
            {
                if (value > MAX_WIDTH || value < 0) throw new ArgumentOutOfRangeException();
                else width = value;
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                if (value > MAX_HEiGHT || value < 0) throw new ArgumentOutOfRangeException();
                else height = value;
            }
        }

        public List<Asteroid> Asteroids { get; set; }

        private Star[] _objs;
        private Bullet _bullet;
        private Random rnd;

        private Game()
		{
            rnd = new Random();
        }

        public static Game getGameObject
        {
            get
            {
                if (game == null)
                {
                    game = new Game();
                }
                return game;
            }
        }

        public BufferedGraphics Buffer { get; set; }

        public void Load ()
		{
            if (Buffer == null) throw new Exception();
            _objs = new Star[STARS_INITIAL_AMOUNT];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(40, 10));
            Asteroids = new List<Asteroid>();
            
            for (int i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(STARS_X_SPAWNPOINT, rnd.Next(0, Height)), new Point(-r, r), new Size(3, 3));
            }

            for (int i = 0; i < ASTEROIDS_INITIAL_AMOUNT; i++)
            {
                Repopulate();
            }


        }
        private void Repopulate()
        {
            if (Asteroids.Count < ASTEROIDS_INITIAL_AMOUNT)
            {
                int r = rnd.Next(5, 50);
                Asteroids.Add(new Asteroid(new Point(Width + ASTEROIDS_X_SPAWNPOINT, rnd.Next(0, Height)), new Point(-r / 5, r), new Size(r, r)));
            }

        }

        public void Init(Form form)
		{
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            form.Width = Width;
            form.Height = Height;
            g = form.CreateGraphics();

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Timer t = new Timer { Interval = 100 };
			t.Start();
			t.Tick += Timer_Tick;

		}

		public void Draw()
		{

			Buffer.Graphics.Clear(Color.Black);

			foreach (Star bo in _objs)
			{
                bo.Draw();
			}
            foreach (Asteroid a in Asteroids)
            {
                a.Draw();
            }
            _bullet.Draw();


			Buffer.Render();
		}

		private void Update()
		{
			foreach (BaseObject bo in _objs)
			{
				bo.Update();
			}
            for (int i = 0; i < Asteroids.Count; i++)
            {
                Asteroid a = Asteroids[i];
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    a.Destroy();
                    Asteroids.RemoveAt(i);
                }
            }
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
            Repopulate();
			Draw();
			Update();
		}

    }
}
