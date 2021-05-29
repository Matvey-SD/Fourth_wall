using System;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Chest : GameObject
    {
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
            var x = Location.X - hero.Location.X;
            var y = Location.Y - hero.Location.Y;
            
            return Math.Sqrt(x * x + y * y) <= 50;
        }
    }
}