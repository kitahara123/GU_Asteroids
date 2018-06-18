using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GU_Asteroids
{
	class Program
	{
		static void Main(string[] args)
		{
			Form form = new Form();
			form.Width = Screen.PrimaryScreen.Bounds.Width;
			form.Height = Screen.PrimaryScreen.Bounds.Width;
			Game.Init(form);
			form.Show();
            Game.Load();
			Game.Draw();
			Application.Run(form);
		}
	}
}
