using System;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Exit : GameObject
    {
        public Location NextMap;
        public readonly Size Collider = new Size(20, 20);
        public Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);

        #region Constructor
        
        public Exit(Point location, Location nextMap) : base(location)
        {
            NextMap = nextMap;
        }

        public Exit(int x, int y, Location nextMap) : base(x, y)
        {
            NextMap = nextMap;
        }
        
        public Exit(Point location) : base(location)
        {
        }

        public Exit(int x, int y) : base(x, y)
        {
        }
        #endregion
        
        public bool IsHeroNear(Hero hero)
        {
            var x = MiddlePoint.X - hero.MiddlePoint.X;
            var y = MiddlePoint.Y - hero.MiddlePoint.Y;

            return Math.Sqrt(x * x + y * y) <= 45;
        }
        
    }
}