using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GU_Asteroids
{
	class WierdFaces : BaseObject
	{
		private Boolean timeToStahp = false;
		private int Size { get; set; }

		public WierdFaces(Point pos, Point dir, int size) : base(pos, dir, new Size())
		{
			this.Size = size;
		}

		public override void Draw()
		{
			Font font = new Font("Arial", Size);
			SolidBrush drawBrush = new SolidBrush(Color.White);
			Game.buffer.Graphics.DrawString("ಠ_ಠ", font, drawBrush, pos);
		}

		public override void Update()
		{

			if (!timeToStahp)
			{
				Size++;
			} else
			{
				Size--;
			}
			
			if (Size > 30)
				timeToStahp = true;
			else if (Size < 10)
				timeToStahp = false;


			base.Update();
		}
	}
}
