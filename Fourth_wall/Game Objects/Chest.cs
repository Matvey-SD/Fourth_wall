using System;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Chest : GameObject
    {
        public readonly Size Collider = new Size(15, 12);
        public Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);

        public bool IsOpened => _isOpened;

        private bool _isOpened = false;
        
        #region Constructor
        public Chest(Point location) : base(location)
        {
        }

        public Chest(int x, int y) : base(x, y)
        {
        }
        #endregion

        public bool CanOpenChest(Location location)
        {
            return location.IsAllEnemiesDead && IsHeroNear(location.Hero);
        }

        public bool IsHeroNear(Hero hero)
        {
            var x = MiddlePoint.X - hero.MiddlePoint.X;
            var y = MiddlePoint.Y - hero.MiddlePoint.Y;

            if (Math.Sqrt(x * x + y * y) <= 15) _isOpened = true;
            return _isOpened;
        }
    }
}