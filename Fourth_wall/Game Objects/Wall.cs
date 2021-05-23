using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Wall : GameObject
    {
        public readonly Size Collider;
        public readonly Image Texture;

        #region Constructor
        public Wall(Point location, Size collider, Image texture) : base(location)
        {
            Collider = collider;
            Texture = texture;
        }
        
        public Wall(Point location, int width, int height, Image texture) : base(location)
        {
            Collider = new Size(width, height);
            Texture = texture;
        }
        
        public Wall(int x, int y, Size collider, Image texture) : base(x, y)
        {
            Collider = collider;
            Texture = texture;
        }
        
        public Wall(int x, int y, int width, int height, Image texture) : base(x, y)
        {
            Collider = new Size(width, height);
            Texture = texture;
        }
        #endregion

        public Point OppositeCorner()
        {
            return new Point(Location.X + Collider.Height, Location.Y + Collider.Width);
        }

        public bool IsPointInside(Point point)
        {
            return (point.X > Location.X && point.X < OppositeCorner().X
                                              && point.Y > Location.Y && point.Y < OppositeCorner().Y);

        }
    }
}