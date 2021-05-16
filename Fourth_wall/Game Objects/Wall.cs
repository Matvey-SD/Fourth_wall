using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Wall : GameObject
    {
        private readonly Size _collider;
        public readonly Image Texture;

        #region Constructor
        public Wall(Point location, Size collider, Image texture) : base(location)
        {
            _collider = collider;
            Texture = texture;
        }
        
        public Wall(Point location, int width, int height, Image texture) : base(location)
        {
            _collider = new Size(width, height);
            Texture = texture;
        }
        
        public Wall(int x, int y, Size collider, Image texture) : base(x, y)
        {
            _collider = collider;
            Texture = texture;
        }
        
        public Wall(int x, int y, int width, int height, Image texture) : base(x, y)
        {
            _collider = new Size(width, height);
            Texture = texture;
        }
        #endregion

        public Point OppositeCorner()
        {
            return new Point(Location.X + _collider.Height, Location.Y + _collider.Width);
        }
    }
}