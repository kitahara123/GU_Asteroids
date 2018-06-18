using System.Drawing;

namespace GU_Asteroids
{
    interface ICollision
    {
        bool Collision(ICollision o);
        Rectangle Rect { get;}
    }
}
