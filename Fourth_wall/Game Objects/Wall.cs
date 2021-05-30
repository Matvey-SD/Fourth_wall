using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Wall : GameObject
    {
        public readonly Size Collider;

        public Point OppositeCorner => new Point(Location.X + Collider.Width, Location.Y + Collider.Height);

        #region Constructor
        public Wall(Point location, Size collider) : base(location)
        {
            Collider = collider;
        }
        
        public Wall(Point location, int width, int height) : base(location)
        {
            Collider = new Size(width, height);
        }
        
        public Wall(int x, int y, Size collider) : base(x, y)
        {
            Collider = collider;
        }
        
        public Wall(int x, int y, int width, int height) : base(x, y)
        {
            Collider = new Size(width, height);
        }
        #endregion

        public bool IsPointInside(Point point)
        {
            return (point.X > Location.X && point.X < OppositeCorner.X
                                              && point.Y > Location.Y && point.Y < OppositeCorner.Y);
        }
    }
}