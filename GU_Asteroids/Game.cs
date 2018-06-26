using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GU_Asteroids
{
	public delegate void Log(String m);

	class Game
	{
        private static Game game = null;
        private const int ASTEROIDS_INITIAL_AMOUNT = 20;
        private const int STARS_INITIAL_AMOUNT = 50;
        private const int ASTEROIDS_X_SPAWNPOINT = -100;
        private const int STARS_X_SPAWNPOINT = 1000;
		private const int BULLET_SPEED = 15;
		private const int MAX_SCORE = 50;

		private const int MAX_WIDTH = 1000;
        private const int MAX_HEiGHT = 1000;

        private BufferedGraphicsContext _context;
		Log Log = new GameLogger().ConsoleLog;

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
		public List<Bullet> Bullets { get; set; }

		private Ship _ship;
		private MedKit _medKit;
		private Star[] _objs;
        private Random rnd;
		private Timer t;

		private int score;


		private Game()
		{
            rnd = new Random();
			
			
		}
		private void GameLogger (Log gl, String m)
		{
			gl(m);
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
			GameLogger(Log, "Load Start");
			Bullets = new List<Bullet>();
			Asteroids = new List<Asteroid>();

			_ship = new Ship(new Point(80, 350), new Point(0, 350), new Point(80, 400), 15);
            _objs = new Star[STARS_INITIAL_AMOUNT];
            
            
            for (int i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(STARS_X_SPAWNPOINT, rnd.Next(0, Height)), new Point(-r, r), new Size(3, 3));
            }

            for (int i = 0; i < ASTEROIDS_INITIAL_AMOUNT; i++)
            {
                int r = rnd.Next(5, 50);
                Asteroids.Add(new Asteroid(new Point(Width + ASTEROIDS_X_SPAWNPOINT, rnd.Next(0, Height)), new Point(-r / 5, r), new Size(r, r)));
            }

            Point p = new Point(40, rnd.Next(100, game.Height - 100));
            _medKit = new MedKit(p, new Point(), new Size(10, 10));

            GameLogger(Log, "Load End");

		}
        private void Repopulate()
        {
            bool b = true;
            foreach (Asteroid a in Asteroids) if (a != null) b=false;
            if (b)
            {
                int r;
                for (int i = 0; i < Asteroids.Count; i++)
                {
                    r = rnd.Next(5, 50);
                    Asteroids[i] = new Asteroid(new Point(Width + ASTEROIDS_X_SPAWNPOINT, rnd.Next(0, Height)), new Point(-r / 5, r), new Size(r, r));
                }
                r = rnd.Next(5, 50);
                Asteroids.Add(new Asteroid(new Point(Width + ASTEROIDS_X_SPAWNPOINT, rnd.Next(0, Height)), new Point(-r / 5, r), new Size(r, r)));
            }
            

			if (_medKit == null)
			{
				Point p = new Point(40, rnd.Next(100, game.Height - 100));
				_medKit = new MedKit(p, new Point(), new Size(10, 10));
				GameLogger(Log, "New medKit spawned at " + p);
			}

        }

        public void Init(Form form)
		{
			GameLogger(Log, "Init Start");
			Graphics g;
            _context = BufferedGraphicsManager.Current;
            form.Width = Width;
            form.Height = Height;
            g = form.CreateGraphics();
			form.KeyDown += Form_KeyDown;


			Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            t = new Timer { Interval = 100 };
			t.Start();
			t.Tick += Timer_Tick;

			Ship.MessageDie += Finish;

			GameLogger(Log, "Game Hight = " + Height);
			GameLogger(Log, "Game Width = " + Width);
			GameLogger(Log, "Init End");
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			
			if (e.KeyCode == Keys.Q) Bullets.Add(new Bullet(_ship.GunPoint, new Point(4, 0), new Size(4, 1), BULLET_SPEED));
			if (e.KeyCode == Keys.Up) _ship.Up();
			if (e.KeyCode == Keys.Down) _ship.Down();
		}

		public void Draw()
		{

			Buffer.Graphics.Clear(Color.Black);

			foreach (Bullet b in Bullets)
			{
				b?.Draw();
			}

			foreach (Star bo in _objs)
			{
                bo.Draw();
			}

            foreach (Asteroid a in Asteroids)
            {
                a?.Draw();
            }
			_ship.Draw();
			Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
			Buffer.Graphics.DrawString("Score:" + score, SystemFonts.DefaultFont, Brushes.White, 100, 0);

			_medKit.Draw();

			Buffer.Render();
		}

		private void Update()
		{
			if (_medKit != null &&_ship.Collision(_medKit))
			{
				_ship.Heal(_medKit.Power);
				_medKit = null;	
			}

            for (int i = 0; i < Bullets.Count; i++)
            {
                var b = Bullets[i];
                b?.Update();
                if (b != null && b.OutOfFrame()) b = null;
            }

			foreach (BaseObject bo in _objs)
			{
				bo.Update();
			}

            for (int iA = 0; iA < Asteroids.Count; iA++)
            {
                if (Asteroids[iA] == null) continue;

                Asteroid a = Asteroids[iA];
                a.Update();
                                
				for (int iB = 0; iB < Bullets.Count; iB++)
				{
					if (Bullets[iB] !=null && a.Collision(Bullets[iB]))
					{
						System.Media.SystemSounds.Hand.Play();
						a.Destroy();
						score += a.Power;
                        Asteroids[iA] = null;
                        Bullets[iB] = null;
					}
				}

				if (_ship.Collision(a))
				{
					_ship.EnergyLow(rnd.Next(1,10));
                    Asteroids[iA] = null;
                    System.Media.SystemSounds.Asterisk.Play();
					if (_ship.Energy <= 0) _ship.Die();

				}

			}
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			if(score >= MAX_SCORE)
			{
				Finish();
				return;
			}
            Repopulate();
			Draw();
			Update();
		}

		public void Finish()
		{
			String  message = "LOL U DIED";
			int size = 60;
			if(score >= MAX_SCORE)
			{
				message = "WINNER WINNER CHICKEN DINNER";
				size = 30;
				
			}
			GameLogger(Log, "Game Over " + message);

			t.Stop();
			Buffer.Graphics.DrawString(message, new Font(FontFamily.GenericSansSerif,
			size, FontStyle.Underline), Brushes.White, 100, 100);
			Buffer.Render();
		}

	}
}
