using System;
using System.Drawing;

namespace Fourth_wall
{
    public class GameObject
    {
        private Point _location;

        public Point Location => _location;

        #region Constructor
        public GameObject(Point location)
        {
            _location = location;
        }
        
        public GameObject(int x, int y)
        {
            _location = new Point(x, y);
        }
        #endregion

        public void ChangeLocation(Point dPoint)
        {
            _location = new Point(_location.X + dPoint.X, _location.Y + dPoint.Y);
        }
        
        public void ChangeLocation(int x, int y)
        {
            _location = new Point(_location.X + x, _location.Y + y);
        }
        
        public void NewLocation(Point dPoint)
        {
            _location = dPoint;
        }
        
        public void NewLocation(int x, int y)
        {
            _location = new Point(x, y);
        }
    }
}