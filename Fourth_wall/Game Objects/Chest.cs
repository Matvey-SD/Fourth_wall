using System;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Chest : GameObject
    {
        public readonly Size Collider = new Size(30, 30);
        public Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);
        
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

        private bool IsHeroNear(Hero hero)
        {
            var x = MiddlePoint.X - hero.MiddlePoint.X;
            var y = MiddlePoint.Y - hero.MiddlePoint.Y;
            
            return Math.Sqrt(x * x + y * y) <= 50;
        }
    }
}