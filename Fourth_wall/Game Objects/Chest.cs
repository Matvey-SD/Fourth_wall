using System;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Chest : GameObject
    {
        public readonly Size Collider = new Size(15, 12);
        private Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);

        public bool IsOpened { get; private set; }

        #region Constructor
        
        public Chest(int x, int y) : base(x, y)
        {
        }
        
        #endregion

        public bool IsHeroNear(Hero hero)
        {
            var x = MiddlePoint.X - hero.MiddlePoint.X;
            var y = MiddlePoint.Y - hero.MiddlePoint.Y;

            if (Math.Sqrt(x * x + y * y) <= 15) IsOpened = true;
            return IsOpened;
        }
    }
}