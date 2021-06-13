using System.Drawing;

namespace Fourth_wall
{
    public class GameObject
    {
        public Point Location { get; private set; }

        #region Constructor

        protected GameObject(Point location) => Location = location;

        protected GameObject(int x, int y) => Location = new Point(x, y);

        #endregion

        protected void ChangeLocation(int x, int y) => 
            Location = new Point(Location.X + x, Location.Y + y);
    }
}