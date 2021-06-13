using System.Drawing;

namespace Fourth_wall
{
    public class GameObject
    {
        private Point _location;

        public Point Location => _location;

        #region Constructor

        protected GameObject(Point location)
        {
            _location = location;
        }

        protected GameObject(int x, int y)
        {
            _location = new Point(x, y);
        }
        #endregion

        protected void ChangeLocation(int x, int y)
        {
            _location = new Point(_location.X + x, _location.Y + y);
        }
    }
}