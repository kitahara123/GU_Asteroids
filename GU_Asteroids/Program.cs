using System.Windows.Forms;

namespace GU_Asteroids
{
	class Program
	{
		static void Main(string[] args)
		{
			Form form = new Form();

            Game go = Game.getGameObject;

            go.Width = 1000;
            go.Height = 600;
            go.Init(form);
			form.Show();
            go.Load();
            go.Draw();
			Application.Run(form);
		}
	}
}
